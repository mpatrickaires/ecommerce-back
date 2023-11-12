using ECommerceBack.Domain.Entities;

namespace ECommerceBack.Domain.Repositories;

public interface ICarrinhoRepository : IRepository<CarrinhoItem>
{
    Task<CarrinhoItem?> BuscarCarrinhoItemAsync(int usuarioId, int itemId);
}
