using DevFreela.Application.Commands.CreateComment;
using FluentValidation;

namespace DevFreela.Application.Validators
{
    public class CreateCommentCommandValidator : AbstractValidator<CreateCommentCommand>
    {
        public CreateCommentCommandValidator()
        {
            RuleFor(pc => pc.IdProject).NotEmpty().WithMessage("É preciso informar o id do projeto.").NotNull().WithMessage("É preciso informar o id do projeto.");
            RuleFor(pc => pc.IdUser).NotEmpty().WithMessage("É preciso informar o id do usuário.").NotNull().WithMessage("É preciso informar o id do usuário.");
            RuleFor(pc => pc.Content).MaximumLength(255).WithMessage("O tamanho máximo de um comentário é de 255 caracteres.");
        }
    }
}
