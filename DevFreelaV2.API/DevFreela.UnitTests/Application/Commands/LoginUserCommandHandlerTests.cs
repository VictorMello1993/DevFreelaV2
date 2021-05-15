using DevFreela.Application.Commands.LoginUser;
using DevFreela.Domain.Entities;
using DevFreela.Domain.Repositories;
using DevFreela.Domain.Services.Auth;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace DevFreela.UnitTests.Application.Commands
{
    public class LoginUserCommandHandlerTests
    {
        [Fact]
        public async Task InputDataIsOk_Executed_ReturnLoginUserViewModel()
        {
            //Arrange
            var loginUserCommand = new LoginUserCommand
            {
                Email = "emaildousuario@gmail.com",
                Password = "123456"
            };

            var user = new User("Nome de teste", loginUserCommand.Email, Convert.ToDateTime("15/12/1993"), loginUserCommand.Password, "client");
            var token = "adsadsa5d56asda564das564da5s64d56a4ds456";

            var authServiceMock = new Mock<IAuthService>();
            var userRepositoryMock = new Mock<IUserRepository>();

            var loginUserCommandHandler = new LoginUserCommandHandler(authServiceMock.Object, userRepositoryMock.Object);

            authServiceMock.Setup(u => u.ComputeSha256Hash(loginUserCommand.Password)).Returns(loginUserCommand.Password);
            authServiceMock.Setup(u => u.GenerateJwtToken(loginUserCommand.Email, user.Role)).Returns(token);
            userRepositoryMock.Setup(u => u.LoginAsync(loginUserCommand.Email, loginUserCommand.Password).Result).Returns(user);
            
            //Act
            var loginUserViewModel = await loginUserCommandHandler.Handle(loginUserCommand, new CancellationToken());

            //Assert
            Assert.NotNull(loginUserViewModel);

            userRepositoryMock.Verify(u => u.LoginAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
            authServiceMock.Verify(a => a.GenerateJwtToken(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }
    }
}
