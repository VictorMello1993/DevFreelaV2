using DevFreela.Application.Commands.CreateProject;
using DevFreela.Application.Queries.GetProjectById;
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
    public class GetProjectByIdQueryHandlerTests
    {
        [Fact]
        public async Task ProjectWithIdExists_Executed_ReturnProjecViewModel()
        {
            //Arrange
            var project = new Project("Nome do projeto", "descrição de teste", 1, 2, 10000);
            var client = new User("User 1", "emaildouser1@gmail.com", Convert.ToDateTime("15/12/1993"), "@123456", "client");
            var freelancer = new User("User 2", "emaildouser2@gmail.com", Convert.ToDateTime("06/03/1998"), "@654321", "freelancer");

            project.SetUserClient(client);
            project.SetUserFreelancer(freelancer);

            var projectRepositoryMock = new Mock<IProjectRepository>();

            var getProjectByIdQuery = new GetProjectByIdQuery(2); //Passando o id 2 para obter um objeto específico
            var getProjectByIdQueryHandler = new GetProjectByIdQueryHandler(projectRepositoryMock.Object);
               
            projectRepositoryMock.Setup(p => p.GetByIdAsync(getProjectByIdQuery.Id).Result).Returns(project);

            //Act            
            var projectDetailsViewModel = await getProjectByIdQueryHandler.Handle(getProjectByIdQuery, new CancellationToken());

            //Assert
            Assert.NotNull(projectDetailsViewModel);            

            projectRepositoryMock.Verify(pr => pr.GetByIdAsync(It.IsAny<int>()), Times.Once);
        }
    }
}
