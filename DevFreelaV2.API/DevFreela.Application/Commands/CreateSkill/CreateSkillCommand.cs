using MediatR;

namespace DevFreela.Application.Commands.CreateSkill
{
    public class CreateSkillCommand : IRequest<int>
    {
        public int Id { get; set; }
        public string Description { get; set; }
    }
}
