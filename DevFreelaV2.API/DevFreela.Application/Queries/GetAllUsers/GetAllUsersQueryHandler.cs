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

namespace DevFreela.Application.Queries.GetAllUsers
{
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, List<UserViewModel>>
    {
        private readonly DevFreelaDbContext _dbContext;

        public GetAllUsersQueryHandler(DevFreelaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<UserViewModel>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            //Entity Framework
            var users = _dbContext.Users;
            var usersViewModel = await users.Select(u => new UserViewModel(u.Id, u.Name, u.Email)).ToListAsync();

            return usersViewModel;

            ////Dapper
            //using (var sqlConnection = new SqlConnection(_connectionString))
            //{
            //    sqlConnection.Open();

            //    var sql = "SELECT Id, Name, Email FROM Users";

            //    return sqlConnection.Query<UserViewModel>(sql).ToList();
            //}
        }
    }
}
