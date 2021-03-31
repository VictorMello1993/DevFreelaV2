using DevFreela.Application.Commands.DeleteUser;
using DevFreela.Application.Queries.GetUserById;
using DevFreela.Domain.Entities;
using DevFreela.Domain.Repositories;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace DevFreela.UnitTests.Application.Commands
{
    public class DeleteUserCommandHandlerTests
    {
        [Fact]
        public async Task UserStatusIsActive_Executed_InactivateUser()
        {
            //Arrange
            var userRepositoryMock = new Mock<IUserRepository>();

            var user = new User("usuário de teste", "usertest@gmail.com", Convert.ToDateTime("15/12/1993"), "123456", "client");
            var id = 4;

            var deleteUserCommand = new DeleteUserCommand(id);
            var deleteUserCommandHandler = new DeleteUserCommandHandler(userRepositoryMock.Object);

            userRepositoryMock.Setup(u => u.GetByIdAsync(id).Result).Returns(user);


            //Act
            await deleteUserCommandHandler.Handle(deleteUserCommand, new CancellationToken());

            //Assert
            userRepositoryMock.Verify(u => u.SaveChangesAsync(), Times.Once);
        }
    }
}
