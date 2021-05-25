using DevFreela.Domain.Services.SendEmail;
using Microsoft.Extensions.Configuration;
using System;
using System.Net;
using System.Net.Mail;

namespace DevFreela.Infrastructure.Services.SendEmail
{
    public class RedeemPasswordService : IRedeemPasswordService
    {
        private readonly IConfiguration _configuration;

        public RedeemPasswordService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void SendMailToRedeemPassword(string userMail, string callbackUrl)
        {
            var client = new SmtpClient()
            {
                Host = _configuration["EmailConfiguration:Host"],
                Port = Convert.ToInt32(_configuration["EmailConfiguration:Port"]),
                EnableSsl = (_configuration["EmailConfiguration:Ssl"] == "S"),
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(_configuration["EmailConfiguration:UserName"], _configuration["EmailConfiguration:Password"])
            };

            var message = new MailMessage();
            message.From = new MailAddress(_configuration["EmailConfiguration:EmailSender"], "Gerenciamento de projetos freelancer");
            message.To.Add(userMail);
            message.Subject = "Redefinição de senha";
            message.Body = string.Format("Redefina a sua senha <a href='{0}'>aqui</a>", callbackUrl);
            message.IsBodyHtml = true;

            client.Send(message);
        }
    }
}
