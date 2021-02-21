using MediatR;

namespace DevFreela.Application.Commands.UpdateUser
{
    public class UpdateUserCommand : IRequest<Unit>
    {
        public UpdateUserCommand(int id, string email)
        {
            Id = id;
            Email = email;
        }

        public int Id { get; private set; }
        public string Email { get; set; }
    }
}
