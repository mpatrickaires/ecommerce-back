using AutoMapper;
using ECommerceBack.Application.Dtos;
using ECommerceBack.Application.Services.Interfaces;
using ECommerceBack.Domain.Repositories;

namespace ECommerceBack.Application.Services;

public class ProdutoService : IProdutoService
{
    private readonly IProdutoRepository _produtoRepository;
    private readonly IMapper _mapper;

    public ProdutoService(IProdutoRepository produtoRepository, IMapper mapper)
    {
        _produtoRepository = produtoRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ProdutoVitrineDto>> ObterVitrineDeProdutos()
    {
        return _mapper.Map<IEnumerable<ProdutoVitrineDto>>(await _produtoRepository
            .BuscarTodosAsync(p => p.Imagens));
    }

    public async Task<ProdutoDetalhesDto> ObterDetalhesProdutoAsync(int produtoId)
    {
        return _mapper.Map<ProdutoDetalhesDto>(await _produtoRepository
            .BuscarPorIdAsync(produtoId, p => p.Imagens, p => p.Itens));
    }
}
