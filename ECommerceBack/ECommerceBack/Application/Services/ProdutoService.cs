using AutoMapper;
using ECommerceBack.Application.Dtos;
using ECommerceBack.Application.Services.Interfaces;
using ECommerceBack.Common.Notification;
using ECommerceBack.Domain.Entities;
using ECommerceBack.Domain.Repositories;

namespace ECommerceBack.Application.Services;

public class ProdutoService : IProdutoService
{
    private readonly IProdutoRepository _produtoRepository;
    private readonly IMapper _mapper;
    private readonly NotificationContext _notificationContext;

    public ProdutoService(IProdutoRepository produtoRepository, IMapper mapper, NotificationContext notificationContext)
    {
        _produtoRepository = produtoRepository;
        _mapper = mapper;
        _notificationContext = notificationContext;
    }

    public async Task<IEnumerable<ProdutoVitrineDto>> ObterVitrineDeProdutos()
    {
        return _mapper.Map<IEnumerable<ProdutoVitrineDto>>(await _produtoRepository
            .BuscarTodosAsync(p => p.Imagens));
    }

    public async Task<ProdutoDetalhesDto?> ObterDetalhesProdutoAsync(int produtoId)
    {
        Produto? produto = await _produtoRepository.BuscarPorIdAsync(produtoId, p => p.Imagens, p => p.Itens);

        if (produto == null)
        {
            _notificationContext.AdicionarNotificacao("Produto não encontrado.");
            return null;
        }

        return _mapper.Map<ProdutoDetalhesDto>(produto);
    }
}
