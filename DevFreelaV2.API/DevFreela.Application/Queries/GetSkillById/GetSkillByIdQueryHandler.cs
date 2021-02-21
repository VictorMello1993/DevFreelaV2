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

namespace DevFreela.Application.Queries.GetSkillById
{
    public class GetSkillByIdQueryHandler : IRequestHandler<GetSkillByIdQuery, SkillViewModel>
    {
        private readonly DevFreelaDbContext _dbContext;

        public GetSkillByIdQueryHandler(DevFreelaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<SkillViewModel> Handle(GetSkillByIdQuery request, CancellationToken cancellationToken)
        {
            //Entity Framework
            var skill = await _dbContext.Skills.SingleOrDefaultAsync(s => s.Id == request.id);

            if (skill == null)
            {
                return null;
            }

            var skillViewModel = new SkillViewModel(skill.Id, skill.Description);

            return skillViewModel;

            //Dapper
            //using (var sqlConnection = new SqlConnection(_connectionString))
            //{
            //    var sql = "SELECT Id, Description FROM Skills WHERE Id = @id";

            //    return sqlConnection.Query<SkillViewModel>(sql, new { Id = id }).SingleOrDefault();
            //}
        }
    }
}
