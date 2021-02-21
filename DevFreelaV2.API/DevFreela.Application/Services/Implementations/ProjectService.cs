using Dapper;
using DevFreela.Application.InputModels;
using DevFreela.Application.Services.Interfaces;
using DevFreela.Application.ViewModels;
using DevFreela.Domain.Entities;
using DevFreela.Infrastructure.Persistence;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;

namespace DevFreela.Application.Services.Implementations
{
    public class ProjectService : IProjectService
    {
        private readonly DevFreelaDbContext _dbContext;
        private readonly string _connectionString;
        public ProjectService(DevFreelaDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _connectionString = configuration.GetConnectionString("DevFreelaV2SQLServer");
        }

        public int Create(NewProjectInputModel inputModel)
        {
            var project = new Project(inputModel.Title,
                                      inputModel.Description,
                                      inputModel.IdClient,
                                      inputModel.IdFreelancer,
                                      inputModel.TotalCost);

            //Entity Framework
            _dbContext.Projects.Add(project);
            _dbContext.SaveChanges();

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

            return project.Id;
        }

        public void CreateComment(CreateCommentInputModel inputModel)
        {
            var comment = new ProjectComment(inputModel.Content, inputModel.IdProject, inputModel.IdUser);

            _dbContext.ProjectComments.Add(comment);

            //Entity Framework
            _dbContext.SaveChanges();

            //Dapper
            //using (var sqlConnection = new SqlConnection(_connectionString))
            //{
            //    sqlConnection.Open();

            //    var sql = "INSERT INTO ProjectComments (Content, IdProject, IdUser, CreatedAt) VALUES (@content, @idproject, @iduser, @createdat)";

            //    sqlConnection.Execute(sql, new { content = comment.Content, idproject = comment.IdProject, iduser = comment.IdUser, createdat = comment.CreatedAt});
            //}
        }

        public void Delete(int id)
        {
            var project = _dbContext.Projects.SingleOrDefault(p => p.Id == id);

            project.Cancel();

            //Entity Framework
            _dbContext.SaveChanges();

            //Dapper
            //using (var sqlConnection = new SqlConnection(_connectionString))
            //{
            //    sqlConnection.Open();

            //    var sql = "UPDATE Projects SET Status = @status WHERE Id = @id";

            //    sqlConnection.Execute(sql, new { status = project.Status, Id = project.Id });
            //}
        }

        public void Finish(int id)
        {
            var project = _dbContext.Projects.SingleOrDefault(p => p.Id == id);

            project.Finish();

            //Entity Framework
            _dbContext.SaveChanges();

            //Dapper
            //using (var sqlConnection = new SqlConnection(_connectionString))
            //{
            //    sqlConnection.Open();

            //    var sql = "UPDATE Projects SET Status = @status, FinishedAt = @finishedat WHERE Id = @id";

            //    sqlConnection.Execute(sql, new { status = project.Status, finishedat = project.FinishedAt, Id = project.Id });
            //}
        }

        public List<ProjectViewModel> GetAll()
        {
            //Entity Framework
            var projects = _dbContext.Projects;
            var projectsViewModel = projects.Select(p => new ProjectViewModel(p.Id, p.Title, p.CreatedAt)).ToList();

            return projectsViewModel;

            //Dapper
            //using (var sqlConnection = new SqlConnection(_connectionString))
            //{
            //    var sql = "SELECT Id, Title, CreatedAt FROM Projects";

            //    return sqlConnection.Query<ProjectViewModel>(sql).ToList();
            //}
        }

        public ProjectDetailsViewModel GetById(int id)
        {
            //EntityFramework
            //var project = _dbContext.Projects.SingleOrDefault(p => p.Id == id); //Forma tradicional - Sem usar propriedades de navegação de User

            //Acessando as propriedades de navegação dos usuários cliente e freelancer            
            //Com include, ela irá adicionar os objetos da entidade User preenchidos para entidade Project
            var project = _dbContext.Projects.Include(p => p.Client)
                                             .Include(p => p.Freelancer)
                                             .SingleOrDefault(p => p.Id == id);

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

        public void Start(int id)
        {
            var project = _dbContext.Projects.SingleOrDefault(p => p.Id == id);

            project.Start();

            //Entity Framework
            _dbContext.SaveChanges();

            //Dapper
            //using (var sqlConnection = new SqlConnection(_connectionString))
            //{
            //    sqlConnection.Open();

            //    var sql = "UPDATE Projects SET Status = @status, StartedAt = @startedat WHERE Id = @id";

            //    sqlConnection.Execute(sql, new { status = project.Status, startedat = project.StartedAt, id = project.Id });
            //}
        }

        public void Update(int id, UpdateProjectInputModel inputModel)
        {
            var project = _dbContext.Projects.SingleOrDefault(p => p.Id == id);

            project.Update(inputModel.Title, inputModel.Description, inputModel.TotalCost);

            //Entity Framework
            _dbContext.SaveChanges();

            //Dapper
            //using (var sqlConnection = new SqlConnection(_connectionString))
            //{
            //    sqlConnection.Open();

            //    var sql = "UPDATE Projects SET Title = @title, Description = @description, TotalCost = @totalcost WHERE Id = @id";

            //    sqlConnection.Execute(sql, new { title = project.Title, description = project.Description, totalcost = project.TotalCost, id = project.Id });
            //}
        }
    }
}
