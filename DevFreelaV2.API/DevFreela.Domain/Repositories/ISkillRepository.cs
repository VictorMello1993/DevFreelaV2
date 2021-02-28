using DevFreela.Domain.DTOs;
using DevFreela.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Domain.Repositories
{
    public interface ISkillRepository
    {
        Task<List<SkillDTO>> GetAllAsync();
        Task<Skill> GetByIdAsync(int id);
    }
}
