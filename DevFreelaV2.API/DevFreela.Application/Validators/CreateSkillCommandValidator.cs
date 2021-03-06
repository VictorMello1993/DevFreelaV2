using DevFreela.Application.Commands.CreateSkill;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace DevFreela.Application.Validators
{
    public class CreateSkillCommandValidator : AbstractValidator<CreateSkillCommand>
    {
        public CreateSkillCommandValidator()
        {
            RuleFor(s => s.Description).MaximumLength(30).WithMessage("Tamanho máximo da descrição é de 30 caracteres.");
            RuleFor(s => s.Description).NotEmpty().WithMessage("Descrição da competência é obrigatória.").NotNull().WithMessage("Descrição da competência é obrigatória.");
        }
    }
}
