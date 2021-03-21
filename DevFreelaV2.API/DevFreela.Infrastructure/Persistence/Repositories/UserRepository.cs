using Dapper;
using DevFreela.Domain.Entities;
using DevFreela.Domain.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Infrastructure.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DevFreelaDbContext _dbContext;
        private readonly string _connectionString;

        public UserRepository(DevFreelaDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _connectionString = configuration.GetConnectionString("DevFreelaV2SQLServer");
        }

        public async Task AddAsync(User user)
        {
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();
        }        

        public async Task<List<User>> GetAllAsync()
        {
            //Entity Framework
            return await _dbContext.Users.ToListAsync();

            //Dapper
            //using (var sqlConnection = new SqlConnection(_connectionString))
            //{
            //    var sql = "SELECT Id, Name, Email FROM Users";

            //    var result = await sqlConnection.QueryAsync<User>(sql);

            //    return result.ToList();
            //}
        }

        public async Task<User> GetByIdAsync(int id)
        {
            //Entity Framework
            return await _dbContext.Users.SingleOrDefaultAsync(u => u.Id == id);

            //Dapper
            //using (var sqlConnection = new SqlConnection(_connectionString))
            //{
            //    var sql = @"SELECT Id, Name, Email FROM Users
            //                WHERE Id = @Id";

            //    var result = await sqlConnection.QueryAsync<User>(sql, new {Id = id});

            //    return result.SingleOrDefault();
            //}
        }

        public async Task<User> LoginAsync(string email, string passwordHash)
        {
            return await _dbContext.Users.SingleOrDefaultAsync(u => u.Email == email && u.Password == passwordHash);
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }        
    }
}
