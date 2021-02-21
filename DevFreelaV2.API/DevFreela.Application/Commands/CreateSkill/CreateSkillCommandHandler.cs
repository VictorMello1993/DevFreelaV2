using DevFreela.Application.Commands.CreateSkill;
using DevFreela.Domain.Entities;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DevFreela.Application.Commands.CreateProject
{
    public class CreateSkillCommandHandler : IRequestHandler<CreateSkillCommand, int>
    {
        private readonly DevFreelaDbContext _dbContext;
        public CreateSkillCommandHandler(DevFreelaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> Handle(CreateSkillCommand request, CancellationToken cancellationToken)
        {
            var skill = new Skill(request.Description);

            //Entity Framework
            await _dbContext.Skills.AddAsync(skill);
            await _dbContext.SaveChangesAsync();

            //Dapper
            //using (var sqlConnection = new SqlConnection(_connectionString))
            //{
            //    sqlConnection.Open();

            //    var sql = "INSERT INTO Skills (Description, CreatedAt) VALUES (@description, @createdat)";

            //    sqlConnection.Execute(sql, new { description = skill.Description, createdat = skill.CreatedAt });
            //}

            return skill.Id;
        }
    }
}
