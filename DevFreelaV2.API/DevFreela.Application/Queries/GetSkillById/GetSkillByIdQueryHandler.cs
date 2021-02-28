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

namespace DevFreela.Application.Queries.GetSkillById
{
    public class GetSkillByIdQueryHandler : IRequestHandler<GetSkillByIdQuery, SkillViewModel>
    {
        private readonly DevFreelaDbContext _dbContext;
        private readonly string _connectionString;
        private readonly ISkillRepository _skillRepository;

        public GetSkillByIdQueryHandler(DevFreelaDbContext dbContext, IConfiguration configuration, ISkillRepository skillRepository)
        {
            _dbContext = dbContext;
            _connectionString = configuration.GetConnectionString("DevFreelaV2SQLServer");
            _skillRepository = skillRepository;
        }

        public async Task<SkillViewModel> Handle(GetSkillByIdQuery request, CancellationToken cancellationToken)
        {
            //Entity Framework - Padrão CQRS
            //var skill = await _dbContext.Skills.SingleOrDefaultAsync(s => s.Id == request.id);

            //if (skill == null)
            //{
            //    return null;
            //}

            //var skillViewModel = new SkillViewModel(skill.Id, skill.Description);

            //return skillViewModel;

            //Dapper - Padrão CQRS
            //using (var sqlConnection = new SqlConnection(_connectionString))
            //{
            //    var sql = "SELECT Id, Description FROM Skills WHERE Id = @id";

            //    return sqlConnection.Query<SkillViewModel>(sql, new { Id = id }).SingleOrDefault();
            //}

            //Padrão Repository
            var skill = await _skillRepository.GetByIdAsync(request.id);

            if(skill == null)
            {
                return null;
            }

            return new SkillViewModel(skill.Id, skill.Description);
        }
    }
}
