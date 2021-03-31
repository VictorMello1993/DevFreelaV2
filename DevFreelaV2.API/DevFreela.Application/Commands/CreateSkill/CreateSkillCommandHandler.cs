using DevFreela.Application.Commands.CreateSkill;
using DevFreela.Domain.Entities;
using DevFreela.Domain.Repositories;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.Extensions.Configuration;
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
        private readonly ISkillRepository _skillRepository;
        private readonly string _connectionString;

        public CreateSkillCommandHandler(ISkillRepository skillRepository)
        {
            _skillRepository = skillRepository;
        }

        public CreateSkillCommandHandler(DevFreelaDbContext dbContext, ISkillRepository skillRepository, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _skillRepository = skillRepository;
            _connectionString = configuration.GetConnectionString("DevFreelaV2SQLServer");
        }

        public async Task<int> Handle(CreateSkillCommand request, CancellationToken cancellationToken)
        {
            var skill = new Skill(request.Description);

            //Entity Framework - Padrão CQRS
            //await _dbContext.Skills.AddAsync(skill);
            //await _dbContext.SaveChangesAsync();

            //Dapper
            //using (var sqlConnection = new SqlConnection(_connectionString))
            //{
            //    sqlConnection.Open();

            //    var sql = "INSERT INTO Skills (Description, CreatedAt) VALUES (@description, @createdat)";

            //    sqlConnection.Execute(sql, new { description = skill.Description, createdat = skill.CreatedAt });
            //}

            //Padrão Repository     
            await _skillRepository.AddAsync(skill);

            return skill.Id;
        }
    }
}
