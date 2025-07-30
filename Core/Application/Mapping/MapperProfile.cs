using Application.Features.Mediator.Commands.UserCommands;
using Application.Features.Mediator.Commands.WeatherDataCommands;
using Application.Features.Mediator.Commands.WeatherStationCommands;
using Application.Features.Mediator.Results.AdminResults;
using Application.Features.Mediator.Results.WeatherDataResults;
using Application.Features.Mediator.Results.WeatherStationResults;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mapping
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<WeatherData, GetWeatherDataByIdQueryResult>().ReverseMap();
            CreateMap<WeatherData, GetAllWeatherDataQueryResult>().ReverseMap();
            CreateMap<WeatherData, CreateWeatherDataCommand>().ReverseMap();
            CreateMap<WeatherData, UpdateWeatherDataCommand>().ReverseMap();
            CreateMap<WeatherData, RemoveWeatherDataCommand>().ReverseMap();


            CreateMap<User, CreateUserCommand>().ReverseMap();
            CreateMap<User, GetAllUserByIsApprovedQueryResult>()
            .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => src.CreatedDate));

            CreateMap<CreateWeatherStationWithDataCommand, WeatherStation>()
    .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => DateTime.Now));

            CreateMap<CreateWeatherStationWithDataCommand, WeatherData>()
                .ForMember(dest => dest.Timestamp, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => DateTime.Now));

            CreateMap<WeatherStation, GetAllWeatherStationsWithLastDataQueryResult>();
            CreateMap<WeatherData, WeatherDataResult>();


        }
    }
}
