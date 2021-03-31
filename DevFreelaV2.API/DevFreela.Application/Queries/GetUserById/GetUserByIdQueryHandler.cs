using DevFreela.Application.ViewModels;
using DevFreela.Domain.Repositories;
using DevFreela.Infrastructure.Persistence;
using DevFreela.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
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
        private readonly IUserRepository _userRepository;
        private readonly string _connectionString;

        public GetUserByIdQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public GetUserByIdQueryHandler(DevFreelaDbContext dbContext, IConfiguration configuration, IUserRepository userRepository)
        {
            _dbContext = dbContext;
            _connectionString = configuration.GetConnectionString("DevFreelaV2SQLServer");
            _userRepository = userRepository;
        }

        public async Task<UserViewModel> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            //Entity Framework - Padrão CQRS
            //var user = await _dbContext.Users.SingleOrDefaultAsync(u => u.Id == request.id);

            //if (user == null)
            //{
            //    return null;
            //}

            //var userViewModel = new UserViewModel(user.Id, user.Name, user.Email);

            //return userViewModel;

            //Dapper
            //using (var sqlConnection = new SqlConnection(_connectionString))
            //{
            //    sqlConnection.Open();

            //    var sql = "SELECT Id, Name, Email FROM Users WHERE Id = @id";

            //    return sqlConnection.Query<UserViewModel>(sql, new { id = id }).SingleOrDefault();
            //}

            //Padrão Repository
            var user = await _userRepository.GetByIdAsync(request.id);

            if(user == null)
            {
                return null;
            }

            return new UserViewModel(user.Id, user.Name, user.Email);
        }
    }
}
