using ECommerceBack.Common.Validators;
using FluentValidation;

namespace ECommerceBack.Domain.Entities;

public class Usuario : Entity<Usuario>
{
    public string Email { get; set; }
    public string Senha { get; set; }
    public string Nome { get; set; }

    protected override AbstractValidator<Usuario> Validador => new UsuarioValidator();
}
