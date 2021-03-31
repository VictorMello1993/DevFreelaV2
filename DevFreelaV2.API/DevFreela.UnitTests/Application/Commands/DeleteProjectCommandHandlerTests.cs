using DevFreela.Application.Commands.DeleteProject;
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
    public class DeleteProjectCommandHandlerTests
    {
        [Fact]
        public async Task ProjectStatusInProgress_Executed_CancelProject()
        {
            //Arrange
            var project = new Project("Nome projeto 1", "descrição do projeto 1", 1, 2, 4000);
            var projectRepositoryMock = new Mock<IProjectRepository>();

            var id = 999999;

            var deleteProjectCommand = new DeleteProjectCommand(id);
            var deleteProjectCommandHandler = new DeleteProjectCommandHandler(projectRepositoryMock.Object);

            projectRepositoryMock.Setup(p => p.GetByIdAsync(id).Result).Returns(project);

            //Act
            await deleteProjectCommandHandler.Handle(deleteProjectCommand, new CancellationToken());

            //Assert
            projectRepositoryMock.Verify(p => p.SaveChangesAsync(), Times.Once);
        }
    }
}
