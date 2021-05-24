using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Domain.Services.SendMail.RedeemPassword
{
    public interface ISendEmailToRedeemPasswordService
    {
        void RedeemPassword(string callbackUrl, string userEmail);
    }
}
