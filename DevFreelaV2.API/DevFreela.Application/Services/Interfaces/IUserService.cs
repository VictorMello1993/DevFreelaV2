using DevFreela.Application.InputModels;
using DevFreela.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace DevFreela.Application.Services.Interfaces
{
    public interface IUserService
    {
        List<UserViewModel> GetAll();
        int Create(NewUserInputModel inputModel);
        UserViewModel GetById(int id);
        void Update(UpdateUserInputModel inputModel);
        void Delete(int id);
    }
}
