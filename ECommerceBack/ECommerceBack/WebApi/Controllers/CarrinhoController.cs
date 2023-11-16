using ECommerceBack.Application.Dtos;
using ECommerceBack.Application.Services.Interfaces;
using ECommerceBack.WebApi.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceBack.WebApi.Controllers;

[Authorize]
[Route("api/carrinhos")]
public class CarrinhoController : ControllerPrincipal
{
    private readonly ICarrinhoService _carrinhoService;

    public CarrinhoController(ICarrinhoService carrinhoService)
    {
        _carrinhoService = carrinhoService;
    }

    [HttpPost]
    public async Task<ActionResult<RespostaApiDto>> AdicionarItemAoCarrinhoAsync([FromBody] CarrinhoItemQuantidadeDto carrinhoItemQuantidadeDto)
    {
        await _carrinhoService.AdicionarItemAoCarrinhoAsync(carrinhoItemQuantidadeDto.ItemId, carrinhoItemQuantidadeDto.Quantidade);
        return new RespostaApiDto("Item adicionado ao carrinho.");
    }

    [HttpPut("quantidade")]
    public async Task<ActionResult<RespostaApiDto>> AlterarQuantidadeItemNoCarrinhoAsync([FromBody] CarrinhoItemQuantidadeDto carrinhoItemQuantidadeDto)
    {
        await _carrinhoService.AlterarQuantidadeItemNoCarrinhoAsync(carrinhoItemQuantidadeDto.ItemId, carrinhoItemQuantidadeDto.Quantidade);
        return new RespostaApiDto("A quantidade do item no carrinho foi alterada.");
    }

    [HttpDelete]
    public async Task<ActionResult<RespostaApiDto>> RemoverItemDoCarrinhoAsync([FromBody] RemoverItemDoCarrinhoDto removerItemDoCarrinhoDto)
    {
        await _carrinhoService.RemoverItemDoCarrinhoAsync(removerItemDoCarrinhoDto.ItemId);
        return new RespostaApiDto("Item removido do carrinho.");
    }

    [HttpGet]
    public async Task<ActionResult<RespostaApiDto<CarrinhoDetalhesDto>>> ObterDetalhesCarrinhoAsync()
    {
        return new RespostaApiDto<CarrinhoDetalhesDto>(await _carrinhoService.ObterDetalhesCarrinhoAsync());
    }

    [HttpGet("quantidade")]
    public async Task<ActionResult<RespostaApiDto<int?>>> ObterQuantidadeItensCarrinhoAsync()
    {
        return new RespostaApiDto<int?>(await _carrinhoService.ObterQuantidadeItensCarrinhoAsync());
    }
}
