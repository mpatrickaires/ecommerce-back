namespace ECommerceBack.Application.Services.Interfaces;

public interface ICarrinhoService
{
    Task AdicionarItemAoCarrinhoAsync(int itemId, int quantidade = 1);
    Task AlterarQuantidadeItemNoCarrinhoAsync(int itemId, int quantidade);
    Task RemoverItemDoCarrinhoAsync(int itemId);
}
