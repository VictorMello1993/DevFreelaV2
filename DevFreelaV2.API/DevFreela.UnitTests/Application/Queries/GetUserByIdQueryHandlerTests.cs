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

namespace DevFreela.UnitTests.Application.Queries
{
    public class GetUserByIdQueryHandlerTests
    {
        [Fact]
        public async Task UserWithIdExists_Executed_ReturnUserViewModel()
        {
            //Arrange
            var user = new User("Usuário de teste", "emaildousuario@gmail.com", Convert.ToDateTime("15/12/1993"), "123456", "client");

            var userRepositoryMock = new Mock<IUserRepository>();

            var getUserByIdQuery = new GetUserByIdQuery(4);
            var getUserByIdQueryHandler = new GetUserByIdQueryHandler(userRepositoryMock.Object);

            userRepositoryMock.Setup(u => u.GetByIdAsync(getUserByIdQuery.id).Result).Returns(user);

            //Act
            var userViewModel = await getUserByIdQueryHandler.Handle(getUserByIdQuery, new CancellationToken());

            //Assert
            Assert.NotNull(userViewModel);

            userRepositoryMock.Verify(pr => pr.GetByIdAsync(It.IsAny<int>()), Times.Once);

        }
    }
}
