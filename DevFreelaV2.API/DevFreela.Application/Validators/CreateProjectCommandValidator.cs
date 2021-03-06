using DevFreela.Application.Commands.CreateProject;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace DevFreela.Application.Validators
{
    public class CreateProjectCommandValidator : AbstractValidator<CreateProjectCommand>
    {
        public CreateProjectCommandValidator()
        {
            RuleFor(p => p.Description).NotEmpty().WithMessage("Descrição é obrigatória.").NotNull().WithMessage("Descrição é obrigatória.");
            RuleFor(p => p.Description).MaximumLength(255).WithMessage("Tamanho máximo da descrição é de 255 caracteres.");

            RuleFor(p => p.Title).NotEmpty().WithMessage("Título é obrigatório.").NotNull().WithMessage("Título é obrigatório.");
            RuleFor(p => p.Title).MaximumLength(30).WithMessage("Tamanho máximo do título é de 30 caracteres.");

            RuleFor(p => p.TotalCost).NotEqual(0).WithMessage("Custo total deve ser diferente de zero.");

            RuleFor(p => p.IdClient).NotEmpty().WithMessage("É preciso especificar o id do usuário cliente.")
                                    .NotEmpty().WithMessage("É preciso especificar o id do usuário cliente.");

            RuleFor(p => p.IdFreelancer).NotEmpty().WithMessage("É preciso especificar o id do usuário freelancer.")
                                        .NotNull().WithMessage("É preciso especificar o id do usuário freelancer.");
        }
    }
}
