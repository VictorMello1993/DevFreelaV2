using DevFreela.Application.InputModels;
using DevFreela.Application.ViewModels;
using System.Collections.Generic;

namespace DevFreela.Application.Services.Interfaces
{
    public interface IUserService
    {
        List<UserViewModel> GetAll();
        int Create(NewUserInputModel inputModel);
        UserViewModel GetById(int id);
        void Update(int id, UpdateUserInputModel inputModel);
        void Delete(int id);
    }
}
