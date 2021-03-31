using DevFreela.Application.Queries.GetAllSkills;
using DevFreela.Domain.DTOs;
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
    public class GetAllSkillsQueryHandlerTests
    {
        [Fact]
        public async Task TwoSkillsExist_Executed_ReturnTwoSkillsDTO()
        {
            //Arrange
            var skills = new List<SkillDTO>
            {
                new SkillDTO(1, "Descrição de teste 1"),
                new SkillDTO(2, "Descrição de teste 2")
            };

            var skillsRepositoryMock = new Mock<ISkillRepository>();
            skillsRepositoryMock.Setup(s => s.GetAllAsync().Result).Returns(skills);

            var getAllSkillsQuery = new GetAllSkillsQuery("");
            var getAllSkillsQueryHandler = new GetAllSkillsQueryHandler(skillsRepositoryMock.Object);

            //Act
            var skillDTOList = await getAllSkillsQueryHandler.Handle(getAllSkillsQuery, new CancellationToken());

            //Assert
            Assert.NotNull(skillDTOList);
            Assert.NotEmpty(skillDTOList);
            Assert.Equal(skills.Count, skillDTOList.Count);

            skillsRepositoryMock.Verify(s => s.GetAllAsync().Result, Times.Once);
        }
    }
}
