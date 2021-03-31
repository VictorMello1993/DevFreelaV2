using DevFreela.Domain.Repositories;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Threading;
using System.Threading.Tasks;

namespace DevFreela.Application.Commands.UpdateUser
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Unit>
    {
        private readonly DevFreelaDbContext _dbContext;
        private readonly IUserRepository _userRepository;
        private readonly string _connectionString;

        public UpdateUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public UpdateUserCommandHandler(DevFreelaDbContext dbContext, IUserRepository userRepository, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _userRepository = userRepository;
            _connectionString = configuration.GetConnectionString("DevFreelaV2SQLServer");
        }

        public async Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            //Padrão CQRS
            //var user = await _dbContext.Users.SingleOrDefaultAsync(u => u.Id == request.Id);

            //user.Update(request.Email);

            //Entity Framework - Padrão CQRS
            //await _dbContext.SaveChangesAsync();

            //Dapper
            //using (var sqlConnection = new SqlConnection(_connectionString))
            //{
            //    sqlConnection.Open();

            //    var sql = "UPDATE Users SET Email = @email WHERE Id = @id";

            //    sqlConnection.Execute(sql, new { email = user.Email, id = user.Id });
            //}

            //Padrão Repository
            var user = await _userRepository.GetByIdAsync(request.Id);

            user.Update(request.Email);

            await _userRepository.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
