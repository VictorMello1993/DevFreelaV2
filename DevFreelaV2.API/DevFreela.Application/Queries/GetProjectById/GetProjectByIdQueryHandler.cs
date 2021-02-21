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

namespace DevFreela.Application.Queries.GetProjectById
{
    public class GetProjectByIdQueryHandler : IRequestHandler<GetProjectByIdQuery, ProjectDetailsViewModel>
    {
        private readonly DevFreelaDbContext _dbContext;

        public GetProjectByIdQueryHandler(DevFreelaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ProjectDetailsViewModel> Handle(GetProjectByIdQuery request, CancellationToken cancellationToken)
        {
            //EntityFramework
            //var project = _dbContext.Projects.SingleOrDefault(p => p.Id == id); //Forma tradicional - Sem usar propriedades de navegação de User

            //Acessando as propriedades de navegação dos usuários cliente e freelancer            
            //Com include, ela irá adicionar os objetos da entidade User preenchidos para entidade Project
            var project = await _dbContext.Projects.Include(p => p.Client)
                                             .Include(p => p.Freelancer)
                                             .SingleOrDefaultAsync(p => p.Id == request.Id);

            if (project == null)
            {
                return null;
            }

            var projectDetailsViewModel = new ProjectDetailsViewModel(project.Id,
                                                                      project.Title,
                                                                      project.Description,
                                                                      project.StartedAt,
                                                                      project.FinishedAt,
                                                                      project.Client.Name,
                                                                      project.Freelancer.Name);

            return projectDetailsViewModel;

            //Dapper
            //using (var sqlConnection = new SqlConnection(_connectionString))
            //{
            //    var sql = "SELECT Id, Title, Description, IdClient, IdFreelancer, StartedAt, FinishedAt FROM Projects WHERE Id = @id";
            //    var sqlUserClient = "SELECT Id, Name FROM Users WHERE Id = @idclient";
            //    var sqlUserFreelancer = "SELECT Id, Name FROM Users WHERE Id = @idfreelancer";

            //    var project = sqlConnection.Query<Project>(sql, new { id = id }).SingleOrDefault();
            //    var client = sqlConnection.Query<User>(sqlUserClient, new { idclient = project != null ? project.IdClient : default(int) }).SingleOrDefault();
            //    var freelancer = sqlConnection.Query<User>(sqlUserFreelancer, new { idfreelancer = project != null ? project.IdFreelancer : default(int) }).SingleOrDefault();

            //    return new ProjectDetailsViewModel(project.Id, project.Title, project.Description, project.StartedAt, project.FinishedAt, 
            //                                       client != null ? client.Name : null, freelancer != null ? freelancer.Name : null);
            //}
        }
    }
}
