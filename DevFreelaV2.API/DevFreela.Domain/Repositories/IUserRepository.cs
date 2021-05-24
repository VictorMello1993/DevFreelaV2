using DevFreela.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Domain.Repositories
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllAsync();
        Task<User> GetByIdAsync(int id);
        Task AddAsync(User user);                
        Task SaveChangesAsync();

        Task<User> LoginAsync(string email, string passwordHash);
        Task<User> GetByEmail(string email);
    }
}
