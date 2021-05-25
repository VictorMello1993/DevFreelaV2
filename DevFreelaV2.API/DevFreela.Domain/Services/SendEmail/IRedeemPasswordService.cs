using System;
using System.Collections.Generic;
using System.Text;

namespace DevFreela.Domain.Services.SendEmail
{
    public interface IRedeemPasswordService
    {
        void SendMailToRedeemPassword(string userMail, string callbackUrl);
    }
}
