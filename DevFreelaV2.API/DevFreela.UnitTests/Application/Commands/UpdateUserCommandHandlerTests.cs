using DevFreela.Application.Commands.UpdateUser;
using DevFreela.Domain.Entities;
using DevFreela.Domain.Repositories;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Xunit;

namespace DevFreela.UnitTests.Application.Commands
{
    public class UpdateUserCommandHandlerTests
    {
        [Fact]
        public async void UserWithIdExists_Executed_UpdateUser()
        {
            //Arrange
            var user = new User("Usuário 1", "emaildousuario1@outlook.com", Convert.ToDateTime("15/12/1993"), "123456", "client");

            var userRepositoryMock = new Mock<IUserRepository>();
            var id = 99999;

            userRepositoryMock.Setup(u => u.GetByIdAsync(id).Result).Returns(user);

            var updateUserCommand = new UpdateUserCommand(id, "emaildousuario3@gmail.com");
            var updateUserCommandHander = new UpdateUserCommandHandler(userRepositoryMock.Object);

            //Act
            await updateUserCommandHander.Handle(updateUserCommand, new CancellationToken());

            //Assert
            userRepositoryMock.Verify(u => u.SaveChangesAsync(), Times.Once);
        }
    }
}
