using Application.Events;
using MassTransit;
using StationWebApi.Configurations;
using System.Net.Mail;
using System.Net;
using Microsoft.Extensions.Options;

namespace StationWebApi.Consumers
{
    public class UserApprovedEventConsumer : IConsumer<UserApprovedEvent>
    {
        private readonly SmtpSettings _smtp;

        public UserApprovedEventConsumer(IOptions<SmtpSettings> smtpOptions)
        {
            _smtp = smtpOptions.Value;
        }

        public async Task Consume(ConsumeContext<UserApprovedEvent> context)
        {
            var msg = context.Message;

            var mail = new MailMessage
            {
                From = new MailAddress(_smtp.UserName),
                Subject = "Profil Onayı",
                Body = $"Merhaba {msg.FullName}, profiliniz yetkili tarafından onaylandı.",
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
