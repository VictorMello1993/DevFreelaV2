using MediatR;

namespace DevFreela.Application.Commands.RedeemPassword
{
    public class RedeemPasswordCommand : IRequest<bool>
    {
        public string Email { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
