using DevFreela.Domain.Repositories;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Threading;
using System.Threading.Tasks;

namespace DevFreela.Application.Commands.DeleteUser
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, Unit>
    {
        private readonly DevFreelaDbContext _dbContext;
        private readonly IUserRepository _userRepository;
        private readonly string _connectionString;

        public DeleteUserCommandHandler(DevFreelaDbContext dbContext, IUserRepository userRepository, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _userRepository = userRepository;
            _connectionString = configuration.GetConnectionString("DevFreelaV2SQLServer");
        }

        public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            //Padrão CQRS
            //var user = await _dbContext.Users.SingleOrDefaultAsync(u => u.Id == request.Id);            

            //Entity Framework - Padrão CQRS
            //await _dbContext.SaveChangesAsync();

            //Dapper
            //using (var sqlConnection = new SqlConnection(_connectionString))
            //{
            //    sqlConnection.Open();

            //    var sql = "UPDATE Users SET Active = @active WHERE Id = @id";

            //    sqlConnection.Execute(sql, new {active = user.Active, id = id});
            //}

            //Padrão Repository
            var user = await _userRepository.GetByIdAsync(request.Id);

            user.Delete();

            await _userRepository.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
