using DevFreela.Application.Commands.UpdateProject;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace DevFreela.Application.Validators
{
    public class UpdateProjectCommandValidator : AbstractValidator<UpdateProjectCommand>
    {
        public UpdateProjectCommandValidator()
        {
            RuleFor(p => p.Description).NotEmpty().WithMessage("Descrição é obrigatória.").NotNull().WithMessage("Descrição é obrigatória.");
            RuleFor(p => p.Description).MaximumLength(255).WithMessage("Tamanho máximo da descrição é de 255 caracteres.");

            RuleFor(p => p.Title).NotEmpty().WithMessage("Título é obrigatório.").NotNull().WithMessage("Título é obrigatório.");
            RuleFor(p => p.Title).MaximumLength(30).WithMessage("Tamanho máximo do título é de 30 caracteres.");
            
            RuleFor(p => p.TotalCost).NotEqual(0).WithMessage("Custo total deve ser diferente de zero.");
        }
    }
}
