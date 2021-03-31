using DevFreela.Application.Commands.CreateUser;
using DevFreela.Domain.Entities;
using DevFreela.Domain.Repositories;
using DevFreela.Domain.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace DevFreela.UnitTests.Application.Commands
{
    public class CreateUserCommandHandlerTests
    {
        [Fact]
        public async Task InputDataIsOk_Executed_ReturnUserId()
        {
            //Arrange
            var createUserCommand = new CreateUserCommand
            {
                BirthDate = Convert.ToDateTime("15/12/1993"),
                Email = "emaildousuario@gmail.com",
                Name = "Usuário 1",
                Password = "123456",
                Role = "client"
            };

            var userRepositoryMock = new Mock<IUserRepository>();
            var authService = new Mock<IAuthService>();

            var createUserCommandHandler = new CreateUserCommandHandler(userRepositoryMock.Object, authService.Object);

            //Act
            var id = await createUserCommandHandler.Handle(createUserCommand, new CancellationToken());

            //Assert
            Assert.True(id >= 0);

            userRepositoryMock.Verify(u => u.AddAsync(It.IsAny<User>()), Times.Once);

        }
    }
}
