using DevFreela.Domain.Entities;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DevFreela.Application.Commands.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, int>
    {
        private readonly DevFreelaDbContext _dbContext;

        public CreateUserCommandHandler(DevFreelaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var user = new User(request.Name, request.Email, request.BirthDate);

            //Entity Framework
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();

            //Dapper
            //using (var sqlConnection = new SqlConnection(_connectionString))
            //{
            //    sqlConnection.Open();

            //    var sql = "INSERT INTO Users (Name, Email, BirthDate, Active, CreatedAt) VALUES(@name, @email, @birthdate, @active, @createdat)";

            //    sqlConnection.Execute(sql, new { name = user.Name, email = user.Email, birthdate = user.BirthDate, active = user.Active, user.CreatedAt});
            //}

            return user.Id;
        }
    }
}
