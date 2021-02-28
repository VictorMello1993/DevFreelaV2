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
    public class ProjectRepository : IProjectRepository
    {
        private readonly DevFreelaDbContext _dbContext;
        private readonly string _connectionString;

        public ProjectRepository(DevFreelaDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _connectionString = configuration.GetConnectionString("DevFreelaV2SQLServer");
        }

        public async Task<List<Project>> GetAllAsync()
        {
            //Entity Framework
            return await _dbContext.Projects.ToListAsync();

            //Dapper
            //using (var sqlConnection = new SqlConnection(_connectionString))
            //{
            //    var sql = "SELECT Id, Title, CreatedAt FROM Projects";

            //    var result = await sqlConnection.QueryAsync<Project>(sql);

            //    return result.ToList();
            //}
        }

        public async Task<Project> GetByIdAsync(int id)
        {
            //Entity Framework
            return await _dbContext.Projects.Include(p => p.Client).Include(p => p.Freelancer).SingleOrDefaultAsync(p => p.Id == id);

            //Dapper
            //using (var sqlConnection = new SqlConnection(_connectionString))
            //{
            //    var sql = @"SELECT P.Id, P.Title, P.Description, U.Name  FROM Projects P, Users U
            //                WHERE P.IdClient = U.Id
            //                OR   P.IdFreelancer = U.Id
            //                AND P.Id = @Id";

            //    var result = await sqlConnection.QueryAsync<Project>(sql, new {Id = id });

            //    return result.SingleOrDefault();
            //}
        }
    }
}
