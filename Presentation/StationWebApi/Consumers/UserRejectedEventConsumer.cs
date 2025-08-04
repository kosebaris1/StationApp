using Application.Events;
using MassTransit;
using Microsoft.Extensions.Options;
using StationWebApi.Configurations;
using System.Net.Mail;
using System.Net;

namespace StationWebApi.Consumers
{
    public class UserRejectedEventConsumer : IConsumer<UserRejectedEvent>
    {
        private readonly SmtpSettings _smtp;

        public UserRejectedEventConsumer(IOptions<SmtpSettings> smtp)
        {
            _smtp = smtp.Value;
        }
        public async Task Consume(ConsumeContext<UserRejectedEvent> context)
        {
            var msg = context.Message;

            var mail = new MailMessage
            {
                From = new MailAddress(_smtp.UserName),
                Subject = "Profil Reddedildi",
                Body = $"Merhaba {msg.FullName}, profiliniz yetkili tarafından reddedildi.",
                IsBodyHtml = false
            };
            mail.To.Add(msg.Email);

            using var client = new SmtpClient(_smtp.Host)
            {
                Port = _smtp.Port,
                Credentials = new NetworkCredential(_smtp.UserName, _smtp.Password),
                EnableSsl = _smtp.EnableSsl
            };

            await client.SendMailAsync(mail);
        }
    }
    
}
