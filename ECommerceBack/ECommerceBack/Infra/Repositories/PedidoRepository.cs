using ECommerceBack.Domain.Entities;
using ECommerceBack.Domain.Repositories;
using ECommerceBack.Infra.Database;
using Microsoft.EntityFrameworkCore;

namespace ECommerceBack.Infra.Repositories;

public class PedidoRepository : RepositoryBase<Pedido>, IPedidoRepository
{
    public PedidoRepository(ECommerceDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Pedido>> BuscarTodosPedidosUsuarioAsync(int usuarioId)
    {
        return await DbSet.Where(p => p.UsuarioId == usuarioId).OrderByDescending(p => p.DataPedido).ToListAsync();
    }
}
