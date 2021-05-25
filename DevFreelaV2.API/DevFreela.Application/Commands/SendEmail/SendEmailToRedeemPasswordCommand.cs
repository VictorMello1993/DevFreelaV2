using MediatR;

namespace DevFreela.Application.Commands.SendMail
{
    public class SendEmailToRedeemPasswordCommand : IRequest<Unit>
    {
        public SendEmailToRedeemPasswordCommand(string email, string callbackUrl)
        {
            Email = email;
            CallbackUrl = callbackUrl;
        }

        public string Email { get; private set; }
        public string CallbackUrl { get; private set; }
    }
}
