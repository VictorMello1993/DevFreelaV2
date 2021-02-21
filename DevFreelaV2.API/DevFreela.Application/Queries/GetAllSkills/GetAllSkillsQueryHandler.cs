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

namespace DevFreela.Application.Queries.GetAllSkills
{
    public class GetAllSkillsQueryHandler : IRequestHandler<GetAllSkillsQuery, List<SkillViewModel>>
    {
        private readonly DevFreelaDbContext _dbContext;

        public GetAllSkillsQueryHandler(DevFreelaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<SkillViewModel>> Handle(GetAllSkillsQuery request, CancellationToken cancellationToken)
        {
            //Entity Framework
            var skills = _dbContext.Skills;
            var skillsViewModel = await skills.Select(p => new SkillViewModel(p.Id, p.Description)).ToListAsync();

            return skillsViewModel;


            //Dapper
            //using (var sqlConnection = new SqlConnection(_connectionString))
            //{                
            //    var sql = "SELECT Id, Description FROM Skills";

            //    return sqlConnection.Query<SkillViewModel>(sql).ToList();
            //}
        }
    }
}
