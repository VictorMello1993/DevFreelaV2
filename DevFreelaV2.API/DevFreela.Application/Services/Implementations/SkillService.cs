//using DevFreela.Application.InputModels;
//using DevFreela.Application.Services.Interfaces;
//using DevFreela.Application.ViewModels;
//using DevFreela.Domain.Entities;
//using DevFreela.Infrastructure.Persistence;
//using Microsoft.Extensions.Configuration;
//using System.Collections.Generic;
//using System.Linq;

//namespace DevFreela.Application.Services.Implementations
//{
//    public class SkillService : ISkillService
//    {
//        private readonly DevFreelaDbContext _dbContext;
//        private readonly string _connectionString;

//        public SkillService(DevFreelaDbContext dbContext, IConfiguration configuration)
//        {
//            _dbContext = dbContext;
//            _connectionString = configuration.GetConnectionString("DevFreelaV2SQLServer");
//        }

//        public List<SkillViewModel> GetAll()
//        {
//            //Entity Framework
//            var skills = _dbContext.Skills;
//            var skillsViewModel = skills.Select(p => new SkillViewModel(p.Id, p.Description)).ToList();

//            return skillsViewModel;


//            //Dapper
//            //using (var sqlConnection = new SqlConnection(_connectionString))
//            //{                
//            //    var sql = "SELECT Id, Description FROM Skills";

//            //    return sqlConnection.Query<SkillViewModel>(sql).ToList();
//            //}
//        }

//        public SkillViewModel GetById(int id)
//        {
//            //Entity Framework
//            var skill = _dbContext.Skills.SingleOrDefault(s => s.Id == id);

//            if (skill == null)
//            {
//                return null;
//            }

//            var skillViewModel = new SkillViewModel(skill.Id, skill.Description);

//            return skillViewModel;

//            //Dapper
//            //using (var sqlConnection = new SqlConnection(_connectionString))
//            //{
//            //    var sql = "SELECT Id, Description FROM Skills WHERE Id = @id";

//            //    return sqlConnection.Query<SkillViewModel>(sql, new { Id = id }).SingleOrDefault();
//            //}
//        } 
        
//        public int Create(NewSkillInputModel inputModel)
//        {
//            var skill = new Skill(inputModel.Description);

//            //Entity Framework
//            _dbContext.Skills.Add(skill);
//            _dbContext.SaveChanges();

//            //Dapper
//            //using (var sqlConnection = new SqlConnection(_connectionString))
//            //{
//            //    sqlConnection.Open();

//            //    var sql = "INSERT INTO Skills (Description, CreatedAt) VALUES (@description, @createdat)";

//            //    sqlConnection.Execute(sql, new { description = skill.Description, createdat = skill.CreatedAt });
//            //}

//            return skill.Id;
//        }
//    }
//}
