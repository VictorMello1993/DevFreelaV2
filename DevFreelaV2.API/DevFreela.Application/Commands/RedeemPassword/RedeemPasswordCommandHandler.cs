using DevFreela.Domain.Repositories;
using DevFreela.Domain.Services.SendEmail;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DevFreela.Application.Commands.RedeemPassword
{
    public class RedeemPasswordCommandHandler : IRequestHandler<RedeemPasswordCommand, Unit>
    {
        private readonly IRedeemPasswordService _redeemPasswordService;
        private readonly IUserRepository _userRepository;

        public RedeemPasswordCommandHandler(IRedeemPasswordService redeemPasswordService)
        {
            _redeemPasswordService = redeemPasswordService;
        }

        public Task<Unit> Handle(RedeemPasswordCommand request, CancellationToken cancellationToken)
        {            
            _redeemPasswordService.SendMailToRedeemPassword(request.Email, request.CallbackUrl);

            return Unit.Task;
        }
    }
}
