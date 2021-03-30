using DevFreela.Application.Queries.GetAllUsers;
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
    public class GetAllUsersCommandHandlerTests
    {
        [Fact]
        public async Task FourUsersExist_Executed_ReturnUserViewModels()
        {
            //Arrange
            var users = new List<User>
            {
                new User("Nome 1", "emaildousuario1@gmail.com", Convert.ToDateTime("15/04/1998"), "123456", "client"),
                new User("Nome 2", "emaildousuario2@gmail.com", Convert.ToDateTime("06/01/2000"), "@151293vsm", "freelancer"),
                new User("Nome 3", "emaildousuario3@gmail.com", Convert.ToDateTime("06/06/1989"), "@6789", "freelancer"),
                new User("Nome 4", "emaildousuario4@gmail.com", Convert.ToDateTime("10/12/1993"), "@151293smv", "client")
            };

            var userRepositoryMock = new Mock<IUserRepository>();
            userRepositoryMock.Setup(s => s.GetAllAsync().Result).Returns(users);

            var getAllUsersQuery = new GetAllUsersQuery("");
            var getAllUsersQueryHandler = new GetAllUsersQueryHandler(userRepositoryMock.Object);

            //Act
            var userViewModelList = await getAllUsersQueryHandler.Handle(getAllUsersQuery, new CancellationToken());

            //Assert
            Assert.NotNull(userViewModelList);
            Assert.NotEmpty(userViewModelList);
            Assert.Equal(users.Count, userViewModelList.Count);

            userRepositoryMock.Verify(u => u.GetAllAsync().Result, Times.Once);
        }
    }
}
