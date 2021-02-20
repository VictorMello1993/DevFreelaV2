using DevFreela.Application.InputModels;
using DevFreela.Application.Services.Interfaces;
using DevFreela.Application.ViewModels;
using DevFreela.Domain.Entities;
using DevFreela.Infrastructure.Persistence;
using System.Collections.Generic;
using System.Linq;

namespace DevFreela.Application.Services.Implementations
{
    public class SkillService : ISkillService
    {
        private readonly DevFreelaDbContext _dbContext;

        public SkillService(DevFreelaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<SkillViewModel> GetAll()
        {
            var skills = _dbContext.Skills;
            var skillsViewModel = skills.Select(p => new SkillViewModel(p.Id, p.Description)).ToList();

            return skillsViewModel;
        }

        public SkillViewModel GetById(int id)
        {
            var skill = _dbContext.Skills.SingleOrDefault(s => s.Id == id);

            if(skill == null)
            {
                return null;
            }

            var skillViewModel = new SkillViewModel(skill.Id, skill.Description);

            return skillViewModel;
        } 
        
        public int Create(NewSkillInputModel inputModel)
        {
            var skill = new Skill(inputModel.Description);

            _dbContext.Skills.Add(skill);
            _dbContext.SaveChanges();

            return skill.Id;
        }
    }
}
