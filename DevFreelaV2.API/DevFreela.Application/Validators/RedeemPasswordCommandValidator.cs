using DevFreela.Application.Commands.RedeemPassword;
using FluentValidation;
using System.Text.RegularExpressions;

namespace DevFreela.Application.Validators
{
    public class RedeemPasswordCommandValidator : AbstractValidator<RedeemPasswordCommand>
    {
        public RedeemPasswordCommandValidator()
        {
            RuleFor(args => args).Custom((args, context) =>
            {
                if (string.IsNullOrWhiteSpace(args.NewPassword) || string.IsNullOrWhiteSpace(args.ConfirmPassword))
                {
                    context.AddFailure("Por favor, preencha e confirme a senha.");
                }
                else
                {
                    if (args.NewPassword != args.ConfirmPassword)
                    {
                        context.AddFailure("As senhas não coincidem.");
                    }

                    if (!ValidPassword(args.NewPassword) || !ValidPassword(args.ConfirmPassword))
                    {
                        context.AddFailure(@"As senhas estão incorretas. Elas devem conter pelo menos 8 caracteres, um número, uma letra maiúscula, uma minúscula, e um caracter especial.");
                    }
                }
            });

            RuleFor(u => u.Email).NotNull().WithMessage("E-mail é obrigatório.").NotEmpty().WithMessage("E-mail é obrigatório.");
            RuleFor(e => e.Email).EmailAddress().When(u => !string.IsNullOrWhiteSpace(u.Email)).WithMessage("E-mail inválido.");
        }

        public bool ValidPassword(string password)
        {
            var regex = new Regex(@"^.*(?=.{8,})(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!*@#$%^&+=]).*$");

            return regex.IsMatch(password);
        }
    }
}
