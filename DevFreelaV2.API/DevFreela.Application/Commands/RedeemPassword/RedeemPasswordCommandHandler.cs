using DevFreela.Domain.Repositories;
using DevFreela.Domain.Services.Auth;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DevFreela.Application.Commands.RedeemPassword
{
    public class RedeemPasswordCommandHandler : IRequestHandler<RedeemPasswordCommand, bool>
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthService _authRepository;

        public RedeemPasswordCommandHandler(IUserRepository userRepository, IAuthService authRepository)
        {
            _userRepository = userRepository;
            _authRepository = authRepository;
        }

        public async Task<bool> Handle(RedeemPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByEmail(request.Email);

            if(user == null)
            {
                return false;
            }

            var hashedPassword = _authRepository.ComputeSha256Hash(request.ConfirmPassword);

            user.UpdatePassword(hashedPassword);

            await _userRepository.SaveChangesAsync();

            return true;
        }
    }
}
