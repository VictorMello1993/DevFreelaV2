using DevFreela.Application.Commands.StartProject;
using DevFreela.Domain.Entities;
using DevFreela.Domain.Repositories;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace DevFreela.UnitTests.Application.Commands
{
    public class StartProjectCommandHandlerTests
    {
        [Fact]
        public async Task ProjectCreated_Executed_StartProject()
        {
            //Arrange
            var project = new Project("Nome do projeto", "descrição", 1, 2, 50000);
            var projectRepositoryMock = new Mock<IProjectRepository>();

            var id = 999999;

            var startProjectCommand = new StartProjectCommand(id);
            var startProjectCommandHandler = new StartProjectCommandHandler(projectRepositoryMock.Object);

            projectRepositoryMock.Setup(p => p.GetByIdAsync(id).Result).Returns(project);

            //Act
            await startProjectCommandHandler.Handle(startProjectCommand, new CancellationToken());

            //Assert
            projectRepositoryMock.Verify(p => p.SaveChangesAsync(), Times.Once);
        }
    }
}
