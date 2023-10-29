using FluentValidation.Results;

namespace ECommerceBack.Common;

public class NotificationContext
{
    private readonly List<string> _notificacoes = new();
    public IReadOnlyCollection<string> Notificacoes => _notificacoes;
    public bool PossuiNotificacoes => _notificacoes.Any();

    public void AdicionarNotificacao(string notificacao) => _notificacoes.Add(notificacao);

    public void AdicionarNotificacoes(ValidationResult validationResult)
        => _notificacoes.AddRange(validationResult.Errors.Select(e => e.ErrorMessage));
}
