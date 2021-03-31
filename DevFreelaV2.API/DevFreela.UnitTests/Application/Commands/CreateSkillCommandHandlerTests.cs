using DevFreela.Application.Commands.CreateProject;
using DevFreela.Application.Commands.CreateSkill;
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
    public class CreateSkillCommandHandlerTests
    {
        [Fact]
        public async Task InputDataIsOk_Executed_ReturnSkillId()
        {
            //Arrange
            var skillRepository = new Mock<ISkillRepository>();

            var createSkillCommand = new CreateSkillCommand
            {
                Description = "Skill de teste"
            };

            var createSkillCommandHandler = new CreateSkillCommandHandler(skillRepository.Object);

            //Act
            var id = await createSkillCommandHandler.Handle(createSkillCommand, new CancellationToken());

            //Assert
            Assert.True(id >= 0);

            skillRepository.Verify(s => s.AddAsync(It.IsAny<Skill>()), Times.Once);
        }
    }
}
