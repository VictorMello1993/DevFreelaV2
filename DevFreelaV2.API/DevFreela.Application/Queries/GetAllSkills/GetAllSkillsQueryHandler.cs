using Dapper;
using DevFreela.Domain.DTOs;
using DevFreela.Domain.Repositories;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DevFreela.Application.Queries.GetAllSkills
{
    public class GetAllSkillsQueryHandler : IRequestHandler<GetAllSkillsQuery, List<SkillDTO>>
    {
        private readonly DevFreelaDbContext _dbContext;
        private readonly string _connectionString;
        private ISkillRepository _skillRepository;

        public GetAllSkillsQueryHandler(ISkillRepository skillRepository)
        {
            _skillRepository = skillRepository;
        }

        public GetAllSkillsQueryHandler(DevFreelaDbContext dbContext, IConfiguration configuration, ISkillRepository skillRepository)
        {
            _dbContext = dbContext;
            _connectionString = configuration.GetConnectionString("DevFreelaV2SQLServer");
            _skillRepository = skillRepository;
        }

        //Padrão CQRS
        //public async Task<List<SkillViewModel>> Handle(GetAllSkillsQuery request, CancellationToken cancellationToken)
        //{
        //    //Entity Framework
        //    //var skills = _dbContext.Skills;
        //    //var skillsViewModel = await skills.Select(p => new SkillViewModel(p.Id, p.Description)).ToListAsync();

        //    //return skillsViewModel;        

        //    //Dapper
        //    //using (var sqlConnection = new SqlConnection(_connectionString))
        //    //{                
        //    //    var sql = "SELECT Id, Description FROM Skills";

        //    //    return sqlConnection.Query<SkillViewModel>(sql).ToList();
        //    //}
        //}

        //Padrão Repository
        public async Task<List<SkillDTO>> Handle(GetAllSkillsQuery request, CancellationToken cancellationToken)
        {
            return await _skillRepository.GetAllAsync();
        }
    }
}
