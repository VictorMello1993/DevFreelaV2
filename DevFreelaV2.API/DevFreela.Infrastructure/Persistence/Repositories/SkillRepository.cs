using Dapper;
using DevFreela.Domain.DTOs;
using DevFreela.Domain.Entities;
using DevFreela.Domain.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevFreela.Infrastructure.Persistence.Repositories
{
    public class SkillRepository : ISkillRepository
    {
        private readonly DevFreelaDbContext _dbContext;
        private readonly string _connectionString;
        public SkillRepository(DevFreelaDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _connectionString = configuration.GetConnectionString("DevFreelaV2SQLServer");
        }

        public async Task<List<SkillDTO>> GetAllAsync()
        {
            //Entity Framework
            var skills = _dbContext.Skills;
            return await skills.Select(p => new SkillDTO(p.Id, p.Description)).ToListAsync();            

            //Dapper
            //using (var sqlConnection = new SqlConnection(_connectionString))
            //{
            //    var sql = "SELECT Id, Description FROM Skills";

            //    var result = await sqlConnection.QueryAsync<SkillDTO>(sql);

            //    return result.ToList();
            //}
        }

        public async Task<Skill> GetByIdAsync(int id)
        {
            //Entity Framework
            //return await _dbContext.Skills.SingleOrDefaultAsync(s => s.Id == id);

            //Dapper
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                var sql = @"SELECT Id, Description FROM Skills
                            WHERE Id = @Id";

                var result = await sqlConnection.QueryAsync<Skill>(sql, new {Id = id });

                return result.SingleOrDefault();
            }
        }
    }
}
