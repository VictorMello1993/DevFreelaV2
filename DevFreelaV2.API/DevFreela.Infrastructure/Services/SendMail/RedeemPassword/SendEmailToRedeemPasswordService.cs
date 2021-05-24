using DevFreela.Domain.Services.Auth;
using DevFreela.Domain.Services.SendMail.RedeemPassword;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Infrastructure.Services.SendMail.RedeemPassword
{
    public class SendEmailToRedeemPasswordService : ISendEmailToRedeemPasswordService
    {
        private readonly IConfiguration _configuration;
        private readonly IAuthService _authService;

        public SendEmailToRedeemPasswordService(IConfiguration configuration, IAuthService authService)
        {
            _configuration = configuration;
            _authService = authService;
        }

        public void RedeemPassword(string callbackUrl, string userEmail)
        {
            var secureStr = new SecureString();
            var arrayStrPassword = _authService.ComputeSha256Hash(_configuration["EmailConfiguration:Password"]).ToCharArray();            

            foreach (var ch in arrayStrPassword)
            {
                secureStr.AppendChar(ch);
            }

            var client = new SmtpClient()
            {
                Host = _configuration["EmailConfiguration:Host"],
                Port = Convert.ToInt32(_configuration["EmailConfiguration:Port"]),
                EnableSsl = (_configuration["EmailConfiguration:Ssl"] == "S"),
                UseDefaultCredentials = false,

                Credentials = new NetworkCredential(_configuration["EmailConfiguration:UserName"], secureStr)
            };

            var message = new MailMessage();

            message.From = new MailAddress(_configuration["EmailConfiguration:EmailSender"], "Suporte DevFreela");
            message.To.Add(userEmail);
            message.Subject = "Redefinição de senha";
            message.Body = string.Format("Redefina a sua senha <a href = '{0}'> aqui</a>", callbackUrl);
            message.IsBodyHtml = true;

            client.Send(message);
        }
    }
}
