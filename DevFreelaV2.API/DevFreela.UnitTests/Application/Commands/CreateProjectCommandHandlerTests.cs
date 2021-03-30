using DevFreela.Application.Commands.CreateProject;
using DevFreela.Domain.Repositories;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using System.Threading;
using DevFreela.Domain.Entities;

namespace DevFreela.UnitTests.Application.Commands
{
    public class CreateProjectCommandHandlerTests
    {
        [Fact]
        public async Task InputDataIsOk_Executed_ReturnProjectId()
        {
            //Arrange
            var projectRepository = new Mock<IProjectRepository>();

            var createProjectCommand = new CreateProjectCommand
            {
                Title = "Titulo de Teste",
                Description = "Uma descrição de teste",
                TotalCost = 50000,
                IdClient = 1,
                IdFreelancer = 2
            };

            var createProjectCommandHandler = new CreateProjectCommandHandler(projectRepository.Object);

            //Act
            var id = await createProjectCommandHandler.Handle(createProjectCommand, new CancellationToken());

            //Assert
            Assert.True(id >= 0); //Verificar se foi retornado o id

            projectRepository.Verify(pr => pr.AddAsync(It.IsAny<Project>()), Times.Once); //Verificando se o método AddAsync foi chamado 1 vez
        }
    }
}
