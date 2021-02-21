using DevFreela.Application.ViewModels;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DevFreela.Application.Queries.GetUserById
{
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserViewModel>
    {
        private readonly DevFreelaDbContext _dbContext;

        public GetUserByIdQueryHandler(DevFreelaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<UserViewModel> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            //Entity Framework
            var user = await _dbContext.Users.SingleOrDefaultAsync(u => u.Id == request.id);

            if (user == null)
            {
                return null;
            }

            var userViewModel = new UserViewModel(user.Id, user.Name, user.Email);

            return userViewModel;

            //Dapper
            //using (var sqlConnection = new SqlConnection(_connectionString))
            //{
            //    sqlConnection.Open();

            //    var sql = "SELECT Id, Name, Email FROM Users WHERE Id = @id";

            //    return sqlConnection.Query<UserViewModel>(sql, new { id = id }).SingleOrDefault();
            //}
        }
    }
}
