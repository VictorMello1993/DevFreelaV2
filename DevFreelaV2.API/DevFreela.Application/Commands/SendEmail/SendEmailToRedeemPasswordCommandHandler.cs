using DevFreela.Domain.Repositories;
using DevFreela.Domain.Services.SendEmail;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DevFreela.Application.Commands.SendMail
{
    public class SendEmailToRedeemPasswordCommandHandler : IRequestHandler<SendEmailToRedeemPasswordCommand, Unit>
    {
        private readonly IRedeemPasswordService _redeemPasswordService;
        private readonly IUserRepository _userRepository;

        public SendEmailToRedeemPasswordCommandHandler(IRedeemPasswordService redeemPasswordService)
        {
            _redeemPasswordService = redeemPasswordService;
        }

        public Task<Unit> Handle(SendEmailToRedeemPasswordCommand request, CancellationToken cancellationToken)
        {            
            _redeemPasswordService.SendMailToRedeemPassword(request.Email, request.CallbackUrl);

            return Unit.Task;
        }
    }
}
