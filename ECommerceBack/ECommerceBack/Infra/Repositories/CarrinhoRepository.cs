using ECommerceBack.Domain.Entities;
using ECommerceBack.Domain.Repositories;
using ECommerceBack.Infra.Database;

namespace ECommerceBack.Infra.Repositories;

public class CarrinhoRepository : RepositoryBase<CarrinhoItem>, ICarrinhoRepository
{
    public CarrinhoRepository(ECommerceDbContext context) : base(context)
    {
    }

    public Task<CarrinhoItem?> BuscarCarrinhoItemAsync(int usuarioId, int itemId) => BuscarPorExpressaoAsync(c =>
        c.UsuarioId == usuarioId && c.ItemId == itemId);
}
