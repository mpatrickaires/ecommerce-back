namespace ECommerceBack.Application.Authentication;

public interface IUsuarioLogado
{
    int Id { get; }
    string Email { get; }
    string Nome { get; }
}
