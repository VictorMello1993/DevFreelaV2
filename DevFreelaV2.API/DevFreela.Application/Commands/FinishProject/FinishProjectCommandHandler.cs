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

namespace DevFreela.Application.Commands.FinishProject
{
    public class FinishProjectCommandHandler : IRequestHandler<FinishProjectCommand, Unit>
    {
        private readonly DevFreelaDbContext _dbContext;
        private readonly IProjectRepository _projectRepository;
        private readonly string _connectionString;

        public FinishProjectCommandHandler(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public FinishProjectCommandHandler(DevFreelaDbContext dbContext, IProjectRepository projectRepository, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _projectRepository = projectRepository;
            _connectionString = configuration.GetConnectionString("DevFreelaV2SQLServer");
        }

        public async Task<Unit> Handle(FinishProjectCommand request, CancellationToken cancellationToken)
        {
            //Padrão CQRS
            //var project = await _dbContext.Projects.SingleOrDefaultAsync(p => p.Id == request.Id);

            //project.Finish();

            //Entity Framework - Padrão CQRS
            //await _dbContext.SaveChangesAsync();

            //Dapper
            //using (var sqlConnection = new SqlConnection(_connectionString))
            //{
            //    sqlConnection.Open();

            //    var sql = "UPDATE Projects SET Status = @status, FinishedAt = @finishedat WHERE Id = @id";

            //    sqlConnection.Execute(sql, new { status = project.Status, finishedat = project.FinishedAt, Id = project.Id });
            //}

            //Padrão Repository
            var project = await _projectRepository.GetByIdAsync(request.Id);

            project.Finish();

            await _projectRepository.SaveChangesAsync();
            
            return Unit.Value;
        }
    }
}
