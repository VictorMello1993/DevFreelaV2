using DevFreela.Domain.Entities;
using DevFreela.Domain.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace DevFreela.UnitTests.Domain.Entities
{
    public class ProjectTests
    {
        [Fact]
        public void TestIfProjectStartWorks()
        {
            var project = new Project("Nome de teste", "Descrição de teste", 1, 2, 10000);

            Assert.Equal(ProjectStatusEnum.Created, project.Status); //Param 1: Valor esperado; Param 2: valor a ser testado            
            Assert.Null(project.StartedAt); //Verificando se a data de inicialização é nula antes de executar o método Start();

            //Antes de inicializar o projeto, certificar-se de que o título do projeto não seja nula ou vazia
            Assert.NotNull(project.Title); 
            Assert.NotEmpty(project.Title);

            //Antes de inicializar o projeto, certificar-se de que a descrição do projeto não seja nula ou vazia
            Assert.NotNull(project.Description);
            Assert.NotEmpty(project.Description);

            project.Start();

            Assert.Equal(ProjectStatusEnum.InProgress, project.Status);  //Após a execução do Start(), certificar-se de que o status do projeto esteja em progresso (In Progress)
            Assert.NotNull(project.StartedAt); //E a data de inicialização não seja mais nula
            
        }
    }
}
