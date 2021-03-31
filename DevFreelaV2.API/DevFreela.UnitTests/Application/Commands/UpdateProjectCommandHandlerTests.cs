using DevFreela.Application.Commands.UpdateProject;
using DevFreela.Domain.Entities;
using DevFreela.Domain.Repositories;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace DevFreela.UnitTests.Application.Commands
{
    public class UpdateProjectCommandHandlerTests
    {
        [Fact]
        public async Task ProjectWithIdExists_Executed_UpdateProject()
        {
            //Arrange
            var project = new Project("Nome do projeto", "Descrição de teste", 1, 2, 50000);
            var id = 99999;

            var projectRepositoryMock = new Mock<IProjectRepository>();

            projectRepositoryMock.Setup(p => p.GetByIdAsync(id).Result).Returns(project);

            var updateProjectCommand = new UpdateProjectCommand(id, "Nome do projeto alterado", "Descrição do projeto alterado", 30000);
            var updateProjectCommandHandler = new UpdateProjectCommandHandler(projectRepositoryMock.Object);

            //Act
            await updateProjectCommandHandler.Handle(updateProjectCommand, new CancellationToken());

            //Assert
            projectRepositoryMock.Verify(p => p.SaveChangesAsync(), Times.Once);

        }
    }
}
