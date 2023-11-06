using ECommerceBack.Domain.Entities;
using FluentValidation;

namespace ECommerceBack.Domain.Validators;

public class UsuarioValidator : AbstractValidator<Usuario>
{
    public UsuarioValidator()
    {
        RuleFor(u => u.Email)
            .NotEmpty()
            .WithName("E-mail")
            .WithMessage(MensagemValidacao.CampoObrigatorio);

        RuleFor(u => u.Email)
            .EmailAddress()
            .WithMessage("E-mail inválido.");

        RuleFor(u => u.Senha)
            .NotEmpty()
            .WithName("Senha")
            .WithMessage(MensagemValidacao.CampoObrigatorio);

        RuleFor(u => u.Nome)
            .NotEmpty()
            .WithName("Nome")
            .WithMessage(MensagemValidacao.CampoObrigatorio);
    }
}
