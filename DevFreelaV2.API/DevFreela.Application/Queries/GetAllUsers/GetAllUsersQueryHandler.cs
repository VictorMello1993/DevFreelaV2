using DevFreela.Application.ViewModels;
using DevFreela.Domain.Repositories;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
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
        private readonly IUserRepository _userRepository;
        private readonly string _connectionString;
            
        public GetAllUsersQueryHandler(DevFreelaDbContext dbContext, IConfiguration configuration, IUserRepository userRepository)
        {
            _dbContext = dbContext;
            _connectionString = configuration.GetConnectionString("DevFreelaV2SQLServer");
            _userRepository = userRepository;
        }

        public async Task<List<UserViewModel>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            //Entity Framework - Padrão CQRS
            //var users = _dbContext.Users;
            //var usersViewModel = await users.Select(u => new UserViewModel(u.Id, u.Name, u.Email)).ToListAsync();

            //return usersViewModel;

            ////Dapper - Padrão CQRS
            //using (var sqlConnection = new SqlConnection(_connectionString))
            //{
            //    sqlConnection.Open();

            //    var sql = "SELECT Id, Name, Email FROM Users";

            //    return sqlConnection.Query<UserViewModel>(sql).ToList();
            //}

            //Padrão Repository
            var users = await _userRepository.GetAllAsync();
            var usersViewModel = users.Select(u => new UserViewModel(u.Id, u.Name, u.Email)).ToList();

            return usersViewModel;
        }
    }
}
