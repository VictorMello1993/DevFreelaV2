using MediatR;

namespace DevFreela.Application.Commands.RedeemPassword
{
    public class RedeemPasswordCommand : IRequest<Unit>
    {
        public RedeemPasswordCommand(string email, string callbackUrl)
        {
            Email = email;
            CallbackUrl = callbackUrl;
        }

        public string Email { get; private set; }
        public string CallbackUrl { get; private set; }
    }
}
