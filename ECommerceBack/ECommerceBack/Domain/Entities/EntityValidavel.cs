using FluentValidation;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace ECommerceBack.Domain.Entities;

public abstract class EntityValidavel<TEntity> : Entity where TEntity : EntityValidavel<TEntity>
{
    [NotMapped]
    protected abstract AbstractValidator<TEntity> Validador { get; }
    [NotMapped]
    protected FluentValidation.Results.ValidationResult? _validationResult;

    [NotMapped]
    public bool EhValido => Validar();
    [NotMapped]
    public bool EhInvalido => !EhValido;
    [NotMapped]
    public FluentValidation.Results.ValidationResult ValidationResult
    {
        get
        {
            if (_validationResult == null)
            {
                Validar();
            }
            return _validationResult;
        }
    }

    [MemberNotNull(nameof(_validationResult))]
    protected bool Validar()
    {
        _validationResult = Validador.Validate((TEntity)this);
        return ValidationResult.IsValid;
    }
}
