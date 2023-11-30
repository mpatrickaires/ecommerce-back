using ECommerceBack.Application.Dtos;

namespace ECommerceBack.Application.Services.Interfaces;

public interface IPedidoService
{
    Task CriarPedidoAsync();
    Task<IEnumerable<PedidoDto>> ObterPedidosAsync();
}
