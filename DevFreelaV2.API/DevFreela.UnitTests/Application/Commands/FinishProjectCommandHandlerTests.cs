using DevFreela.Application.Commands.FinishProject;
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
    public class FinishProjectCommandHandlerTests
    {
        [Fact]
        public async Task ProjectStatusInProgress_Executed_FinishProject()
        {
            //Arrange
            var project = new Project("Nome do projeto", "descrição do projeto de teste", 1, 2, 50000);

            var projectRepositoryMock = new Mock<IProjectRepository>();
            var id = 9999;

            var finishProjectCommand = new FinishProjectCommand { Id = id };
            var finishProjectCommandHandler = new FinishProjectCommandHandler(projectRepositoryMock.Object);

            projectRepositoryMock.Setup(p => p.GetByIdAsync(id).Result).Returns(project);

            //Act
            await finishProjectCommandHandler.Handle(finishProjectCommand, new CancellationToken());

            //Assert
            projectRepositoryMock.Verify(p => p.SaveChangesAsync(), Times.Once);
        }
    }
}
