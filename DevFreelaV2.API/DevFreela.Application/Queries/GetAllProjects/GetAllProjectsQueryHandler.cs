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

namespace DevFreela.Application.Queries.GetAllProjects
{
    public class GetAllProjectsQueryHandler : IRequestHandler<GetAllProjectsQuery, List<ProjectViewModel>>
    {
        private readonly DevFreelaDbContext _dbContext;

        public GetAllProjectsQueryHandler(DevFreelaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<ProjectViewModel>> Handle(GetAllProjectsQuery request, CancellationToken cancellationToken)
        {
            //Entity Framework
            var projects = _dbContext.Projects;
            var projectsViewModel = await projects.Select(p => new ProjectViewModel(p.Id, p.Title, p.CreatedAt)).ToListAsync();

            return projectsViewModel;

            //Dapper
            //using (var sqlConnection = new SqlConnection(_connectionString))
            //{
            //    var sql = "SELECT Id, Title, CreatedAt FROM Projects";

            //    return sqlConnection.Query<ProjectViewModel>(sql).ToList();
            //}
        }
    }
}
