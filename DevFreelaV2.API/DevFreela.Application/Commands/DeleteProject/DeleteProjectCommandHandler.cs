using DevFreela.Domain.Repositories;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DevFreela.Application.Commands.DeleteProject
{
    public class DeleteProjectCommandHandler : IRequestHandler<DeleteProjectCommand, Unit>
    {
        private readonly DevFreelaDbContext _dbContext;
        private readonly IProjectRepository _projectRepository;
        private string _connectionString;        

        public DeleteProjectCommandHandler(DevFreelaDbContext dbContext, IProjectRepository projectRepository, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _projectRepository = projectRepository;
            _connectionString = configuration.GetConnectionString("DevFreelaV2SQLServer");
        }

        public async Task<Unit> Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
        {
            //Padrão CQRS
            //var project = _dbContext.Projects.SingleOrDefault(p => p.Id == request.Id);

            //Entity Framework - Padrão CQRS
            //await _dbContext.SaveChangesAsync();

            //Dapper - Padrão CQRS
            //using (var sqlConnection = new SqlConnection(_connectionString))
            //{
            //    sqlConnection.Open();

            //    var sql = "UPDATE Projects SET Status = @status WHERE Id = @id";

            //    sqlConnection.Execute(sql, new { status = project.Status, Id = project.Id });
            //}

            //Padrão Repository
            var project = await _projectRepository.GetByIdAsync(request.Id);

            project.Cancel();

            await _projectRepository.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
