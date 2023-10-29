using ECommerceBack.Domain.Entities;
using FluentValidation;

namespace ECommerceBack.Common.Validators;

public class UsuarioValidator : AbstractValidator<Usuario>
{
    public UsuarioValidator()
    {
        RuleFor(u => u.Email)
            .NotEmpty()
            .WithMessage("O e-mail é obrigatório.");

        RuleFor(u => u.Email)
            .EmailAddress()
            .WithMessage("Email inválido.");

        RuleFor(u => u.Senha)
            .NotEmpty()
            .WithMessage("A senha é obrigatória.");

        RuleFor(u => u.Nome)
            .NotEmpty()
            .WithMessage("O nome é obrigatório.");
    }
}
