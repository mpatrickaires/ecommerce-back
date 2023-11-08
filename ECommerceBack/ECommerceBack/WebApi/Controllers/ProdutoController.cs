using ECommerceBack.Application.Dtos;
using ECommerceBack.Application.Services.Interfaces;
using ECommerceBack.WebApi.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceBack.WebApi.Controllers;

[ApiController]
[Route("api/produtos")]
public class ProdutoController : ControllerBase
{
    private readonly IProdutoService _produtoService;

    public ProdutoController(IProdutoService produtoService)
    {
        _produtoService = produtoService;
    }

    [HttpGet]
    public async Task<ActionResult<RespostaApiDto<IEnumerable<ProdutoVitrineDto>>>> BuscarVitrineProdutos()
    {
        return new RespostaApiDto<IEnumerable<ProdutoVitrineDto>>(await
            _produtoService.ObterVitrineDeProdutos());
    }

    [HttpGet("{produtoId}")]
    public async Task<ActionResult<RespostaApiDto<ProdutoDetalhesDto>>> BuscarDetalhesProduto(int produtoId)
    {
        return new RespostaApiDto<ProdutoDetalhesDto>(await _produtoService
            .ObterDetalhesProdutoAsync(produtoId));
    }

}
