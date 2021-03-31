using DevFreela.Application.Queries.GetAllProjects;
using DevFreela.Domain.Entities;
using DevFreela.Domain.Repositories;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using System.Threading;

namespace DevFreela.UnitTests.Application.Queries
{
    public class GetAllProjectsQueryHandlerTests 
    {
        [Fact]
        public async Task ThreeProjectsExist_Executed_ReturnThreeProjectViewModels() //Aplicando o padrão PADRÃO GIVE WHEN THEN
        {
            //Padrão AAA
            //Arrange
            var projects = new List<Project>
            {
                new Project("Nome do projeto 1", "Descricao de teste", 1, 2, 10000),
                new Project("Nome do projeto 2", "Descricao de teste", 1, 2, 10000),
                new Project("Nome do projeto 3", "Descricao de teste", 1, 2, 10000),
            };

            var projectRepositoryMock = new Mock<IProjectRepository>();
            projectRepositoryMock.Setup(pr => pr.GetAllAsync().Result).Returns(projects);

            var getAllProjectsQuery = new GetAllProjectsQuery("");
            var getAllProjectsQueryHandler = new GetAllProjectsQueryHandler(projectRepositoryMock.Object);

            //Act
            var projectViewModelList = await getAllProjectsQueryHandler.Handle(getAllProjectsQuery, new CancellationToken());

            //Assert
            Assert.NotNull(projectViewModelList);
            Assert.NotEmpty(projectViewModelList);
            Assert.Equal(projects.Count, projectViewModelList.Count); //Verificar se foi retornada uma lista de 3 projetos no view model

            projectRepositoryMock.Verify(pr => pr.GetAllAsync().Result, Times.Once); //Verificar se o método GetAllAsync foi chamado uma vez
        } 
    }
}
