using ECommerceBack.Domain.Entities;
using ECommerceBack.Domain.Repositories;
using ECommerceBack.Infra.Database;

namespace ECommerceBack.Infra.Repositories;

public class PedidoRepository : RepositoryBase<Pedido>, IPedidoRepository
{
    public PedidoRepository(ECommerceDbContext context) : base(context)
    {
    }
}
