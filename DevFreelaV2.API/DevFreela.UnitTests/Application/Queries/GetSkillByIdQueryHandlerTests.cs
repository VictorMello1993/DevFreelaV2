using DevFreela.Application.Queries.GetSkillById;
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
    public class GetSkillByIdQueryHandlerTests
    {
        [Fact]
        public async Task SkillWithIdExists_Executed_ReturnSkillViewModel()
        {
            //Arrange
            var skill = new Skill("Skill de teste");

            var skillRepositoryMock = new Mock<ISkillRepository>();

            var getSkillByIdQuery = new GetSkillByIdQuery(4);
            var getSkillByIdQueryHander = new GetSkillByIdQueryHandler(skillRepositoryMock.Object);

            skillRepositoryMock.Setup(s => s.GetByIdAsync(getSkillByIdQuery.id).Result).Returns(skill);

            //Act
            var skillViewModel = await getSkillByIdQueryHander.Handle(getSkillByIdQuery, new CancellationToken());

            //Assert
            Assert.NotNull(skillViewModel);

            skillRepositoryMock.Verify(pr => pr.GetByIdAsync(It.IsAny<int>()), Times.Once);
        }
    }
}
