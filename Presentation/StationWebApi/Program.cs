using Application.Interfaces;
using Application.Interfaces.EventPublisherInterface;
using Application.Interfaces.JwtService;
using Application.Interfaces.SignalRInterface;
using Application.Interfaces.WeatherDataInterface;
using Application.Interfaces.WeatherStationInterface;
using Application.Services;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Persistence.Broadcasters;
using Persistence.Context;
using Persistence.Hubs;
using Persistence.Messaging;
using Persistence.Repository;
using Persistence.Repository.WeatherDataRepository;
using Persistence.Repository.WeatherStationRepositories;
using Persistence.Service;
using Persistence.Settings;
using StationWebApi.Configurations;
using StationWebApi.Consumers;
using System.Text;

namespace StationWebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Services
            builder.Services.AddApplicationService(builder.Configuration);
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Station API", Version = "v1" });

                var jwtSecurityScheme = new OpenApiSecurityScheme
                {
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Description = "Put **_ONLY_** your JWT Bearer token below!",
                    Reference = new OpenApiReference
                    {
                        Id = JwtBearerDefaults.AuthenticationScheme,
                        Type = ReferenceType.SecurityScheme
                    }
                };

                c.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        { jwtSecurityScheme, Array.Empty<string>() }
    });
            });

            // DI
            builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IWeatherStationRepository, WeatherStationRepository>();
            builder.Services.AddScoped<IWeatherDataBroadcaster, WeatherDataBroadcaster>();
            builder.Services.AddScoped<IWeatherDataRepository, WeatherDataRepository>();
            builder.Services.AddScoped<IEventPublisher, MassTransitEventPublisher>();


            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));
            builder.Services.AddScoped<IJwtService, JwtService>();

            builder.Services.Configure<SmtpSettings>(builder.Configuration.GetSection("SmtpSettings"));
            builder.Services.AddScoped<IEventPublisher, MassTransitEventPublisher>();
            builder.Services.AddMassTransit(x =>
            {
                x.AddConsumer<UserApprovedEventConsumer>();
                x.AddConsumer<UserRejectedEventConsumer>();

                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host("localhost", "/", h =>
                    {
                        h.Username("guest");
                        h.Password("guest");
                    });

                    cfg.ReceiveEndpoint("user-approved-event-queue", e =>
                    {
                        e.ConfigureConsumer<UserApprovedEventConsumer>(context);
                    });

                    cfg.ReceiveEndpoint("user-rejected-event-queue", e =>
                    {
                        e.ConfigureConsumer<UserRejectedEventConsumer>(context);
                    });
                });
            });

            builder.Services.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", options =>
                {
                    var jwtSettings = builder.Configuration.GetSection("JwtSettings").Get<JwtSettings>();

                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtSettings.Issuer,
                        ValidAudience = jwtSettings.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key))
                    };
                    options.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            var accessToken = context.Request.Query["access_token"];
                            var path = context.HttpContext.Request.Path;

                            if (!string.IsNullOrEmpty(accessToken) &&
                                path.StartsWithSegments("/weatherDataHub"))
                            {
                                context.Token = accessToken;
                                Console.WriteLine($"? Token bulundu: {accessToken}");
                            }
                            else
                            {
                                Console.WriteLine("? Token alýnamadý veya path uyuþmuyor.");
                            }

                            return Task.CompletedTask;
                        }
                    };
                });

            builder.Services.AddAuthorization();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowFrontend", policy =>
                {
                    policy.WithOrigins(
                        "http://localhost:3000",
                        "http://127.0.0.1:3000"
                        ) //  "http://127.0.0.1:5500"
                          .AllowAnyHeader()
                          .AllowAnyMethod()
                          .AllowCredentials(); 
                });
            });

            builder.Services.AddSignalR();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseCors("AllowFrontend");
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            
            app.MapHub<WeatherDataHub>("/weatherDataHub");

            app.Run();
        }
    }
}
