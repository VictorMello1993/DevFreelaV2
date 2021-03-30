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
    public class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommand, int>
    {
        private readonly DevFreelaDbContext _dbContext;
        private readonly string _connectionString;
        private readonly IProjectRepository _projectRepository;
        
        public CreateProjectCommandHandler(DevFreelaDbContext dbContext, IConfiguration configuration, IProjectRepository projectRepository)
        {
            _dbContext = dbContext;
            _connectionString = configuration.GetConnectionString("DevFreelaV2SQLServer");
            _projectRepository = projectRepository;
        }

        public CreateProjectCommandHandler(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<int> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
        {
            var project = new Project(request.Title,
                                      request.Description,
                                      request.IdClient,
                                      request.IdFreelancer,
                                      request.TotalCost);

            //Entity Framework - Padrão CQRS
            //await _dbContext.Projects.AddAsync(project);
            //await _dbContext.SaveChangesAsync();

            //Dapper
            //using (var sqlConnection = new SqlConnection(_connectionString))
            //{
            //    sqlConnection.Open();

            //    var sql = @"INSERT INTO Projects (Title, Description, IdClient, IdFreelancer, TotalCost, CreatedAt, Status) 
            //                VALUES (@title, @description, @idclient, @idfreelancer, @totalcost, @createdat, @status)";

            //    sqlConnection.Execute(sql, new
            //    {
            //        title = project.Title,
            //        description = project.Description,
            //        idclient = project.IdClient,
            //        idfreelancer = project.IdFreelancer,
            //        totalcost = project.TotalCost,
            //        createdat = project.CreatedAt,
            //        status = project.Status
            //    });
            //}

            //Padrão repository
            await _projectRepository.AddAsync(project);

            return project.Id;
        }
    }
}
