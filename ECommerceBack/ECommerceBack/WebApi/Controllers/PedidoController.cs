using ECommerceBack.Application.Dtos;
using ECommerceBack.Application.Services.Interfaces;
using ECommerceBack.WebApi.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceBack.WebApi.Controllers;

[Route("api/pedidos")]
[Authorize]
public class PedidoController : ControllerPrincipal
{
    private readonly IPedidoService _pedidoService;

    public PedidoController(IPedidoService pedidoService)
    {
        _pedidoService = pedidoService;
    }

    [HttpPost]
    public async Task<RespostaApiDto> CriarPedidoAsync()
    {
        await _pedidoService.CriarPedidoAsync();
        return new RespostaApiDto("Pedido criado com sucesso.");
    }

    [HttpGet]
    public async Task<RespostaApiDto<IEnumerable<PedidoDto>>> ObterPedidosAsync()
    {
        return new RespostaApiDto<IEnumerable<PedidoDto>>(await _pedidoService.ObterPedidosAsync());
    }
}
