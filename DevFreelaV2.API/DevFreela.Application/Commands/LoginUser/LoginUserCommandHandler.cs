using DevFreela.Application.ViewModels;
using DevFreela.Domain.Repositories;
using DevFreela.Domain.Services;
using DevFreela.Domain.Services.Auth;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DevFreela.Application.Commands.LoginUser
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, LoginUserViewModel>
    {
        private readonly IAuthService _authService;
        private readonly IUserRepository _userRepository;
        public LoginUserCommandHandler(IAuthService authService, IUserRepository userRepository)
        {
            _authService = authService;
            _userRepository = userRepository;
        }

        public async Task<LoginUserViewModel> Handle(LoginUserCommand request, CancellationToken cancellation)
        {
            //Utilizar o mesmo algoritmo de criptiografia para gerar o hash da senha
            var passwordHash = _authService.ComputeSha256Hash(request.Password);

            //Consultar usuário  no banco de dados que tenha e-mail e senha já encriptada
            var user = await _userRepository.LoginAsync(request.Email, passwordHash);

            //Se não existir, retorna erro no login
            if(user == null)
            {
                return null;
            }

            //Se existir, gerar token de autenticação e autorização usando os mesmos dados de usuário que foi obtido do banco de dados
            var token = _authService.GenerateJwtToken(user.Email, user.Role);

            return new LoginUserViewModel(user.Email, token);                     
        }
    }
}
