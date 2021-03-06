using DevFreela.Application.Commands.CreateUser;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace DevFreela.Application.Validators
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        String message = string.Empty;        
        public CreateUserCommandValidator()
        {
            RuleFor(u => u.Email).EmailAddress().When(u => !string.IsNullOrWhiteSpace(u.Email)).WithMessage("E-mail inválido.");
            RuleFor(u => u.Email).NotNull().WithMessage("E-mail é obrigatório.").NotEmpty().WithMessage("E-mail é obrigatório.");

            RuleFor(u => u.Password).Must(ValidPassword).WithMessage(@"Senha incorreta. Ela deve conter pelo menos 8 caracteres, um número, uma letra maiúscula, uma 
                                                                       minúscula, e um caracter especial.");

            RuleFor(u => u.Name).NotEmpty().WithMessage("Nome é obrigatório").NotNull().WithMessage("Nome é obrigatório.");
        }
        
        public bool ValidPassword(string password)
        {
            var regex = new Regex(@"^.*(?=.{8,})(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!*@#$%^&+=]).*$");

            return regex.IsMatch(password);
        }        
    }
}
