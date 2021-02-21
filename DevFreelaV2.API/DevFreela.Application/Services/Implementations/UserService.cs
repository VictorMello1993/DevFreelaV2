using DevFreela.Application.InputModels;
using DevFreela.Application.Services.Interfaces;
using DevFreela.Application.ViewModels;
using DevFreela.Domain.Entities;
using DevFreela.Infrastructure.Persistence;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;

namespace DevFreela.Application.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly DevFreelaDbContext _dbContext;
        private readonly string _connectionString;

        public UserService(DevFreelaDbContext dbContext, IConfiguration connectionstring)
        {
            _dbContext = dbContext;
            _connectionString = connectionstring.GetConnectionString("DevFreelaV2SQLServer");
        }

        public int Create(NewUserInputModel inputModel)
        {
            var user = new User(inputModel.Name, inputModel.Email, inputModel.BirthDate);

            //Entity Framework
            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();

            //Dapper
            //using (var sqlConnection = new SqlConnection(_connectionString))
            //{
            //    sqlConnection.Open();

            //    var sql = "INSERT INTO Users (Name, Email, BirthDate, Active, CreatedAt) VALUES(@name, @email, @birthdate, @active, @createdat)";

            //    sqlConnection.Execute(sql, new { name = user.Name, email = user.Email, birthdate = user.BirthDate, active = user.Active, user.CreatedAt});
            //}

            return user.Id;
        }

        public void Delete(int id)
        {
            var user = _dbContext.Users.SingleOrDefault(u => u.Id == id);

            user.Delete();

            //EntityFramework
            _dbContext.SaveChanges();

            //Dapper
            //using (var sqlConnection = new SqlConnection(_connectionString))
            //{
            //    sqlConnection.Open();

            //    var sql = "UPDATE Users SET Active = @active WHERE Id = @id";

            //    sqlConnection.Execute(sql, new {active = user.Active, id = id});
            //}
        }

        public List<UserViewModel> GetAll()
        {
            //Entity Framework
            var users = _dbContext.Users;
            var usersViewModel = users.Select(u => new UserViewModel(u.Id, u.Name, u.Email)).ToList();

            return usersViewModel;

            ////Dapper
            //using (var sqlConnection = new SqlConnection(_connectionString))
            //{
            //    sqlConnection.Open();

            //    var sql = "SELECT Id, Name, Email FROM Users";

            //    return sqlConnection.Query<UserViewModel>(sql).ToList();
            //}
        }

        public UserViewModel GetById(int id)
        {
            //Entity Framework
            var user = _dbContext.Users.SingleOrDefault(u => u.Id == id);

            if (user == null)
            {
                return null;
            }

            var userViewModel = new UserViewModel(user.Id, user.Name, user.Email);

            return userViewModel;

            //Dapper
            //using (var sqlConnection = new SqlConnection(_connectionString))
            //{
            //    sqlConnection.Open();

            //    var sql = "SELECT Id, Name, Email FROM Users WHERE Id = @id";

            //    return sqlConnection.Query<UserViewModel>(sql, new { id = id }).SingleOrDefault();
            //}
        }

        public void Update(int id, UpdateUserInputModel inputModel)
        {
            var user = _dbContext.Users.SingleOrDefault(u => u.Id == id);

            user.Update(inputModel.Email);

            //Entity Framework
            _dbContext.SaveChanges();

            //Dapper
            //using (var sqlConnection = new SqlConnection(_connectionString))
            //{
            //    sqlConnection.Open();

            //    var sql = "UPDATE Users SET Email = @email WHERE Id = @id";

            //    sqlConnection.Execute(sql, new { email = user.Email, id = user.Id });
            //}
        }        
    }
}
