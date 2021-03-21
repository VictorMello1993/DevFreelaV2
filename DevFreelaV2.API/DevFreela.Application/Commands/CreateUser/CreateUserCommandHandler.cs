using DevFreela.Domain.Entities;
using DevFreela.Domain.Repositories;
using DevFreela.Domain.Services;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.Extensions.Configuration;
using System.Threading;
using System.Threading.Tasks;

namespace DevFreela.Application.Commands.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, int>
    {
        private readonly DevFreelaDbContext _dbContext;
        private readonly IUserRepository _userRepository;
        private readonly string _connectionstring;
        private readonly IAuthService _authService;

        public CreateUserCommandHandler(DevFreelaDbContext dbContext, IUserRepository userRepository, IConfiguration configuration, IAuthService authService)
        {
            _dbContext = dbContext;
            _userRepository = userRepository;
            _connectionstring = configuration.GetConnectionString("DevFreelaV2SQLServer");
            _authService = authService;
        }

        public async Task<int> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var passwordHash = _authService.ComputeSha256Hash(request.Password);

            var user = new User(request.Name, request.Email, request.BirthDate, passwordHash, request.Role);

            //Entity Framework - Padrão CQRS
            //await _dbContext.Users.AddAsync(user);
            //await _dbContext.SaveChangesAsync();

            //Dapper
            //using (var sqlConnection = new SqlConnection(_connectionString))
            //{
            //    sqlConnection.Open();

            //    var sql = "INSERT INTO Users (Name, Email, BirthDate, Active, CreatedAt) VALUES(@name, @email, @birthdate, @active, @createdat)";

            //    sqlConnection.Execute(sql, new { name = user.Name, email = user.Email, birthdate = user.BirthDate, active = user.Active, user.CreatedAt});
            //}

            //Padrão Repository
            await _userRepository.AddAsync(user);

            return user.Id;
        }
    }
}
