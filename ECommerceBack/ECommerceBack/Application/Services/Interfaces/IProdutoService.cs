using ECommerceBack.Application.Dtos;

namespace ECommerceBack.Application.Services.Interfaces;

public interface IProdutoService
{
    Task<IEnumerable<ProdutoVitrineDto>> ObterVitrineDeProdutos();
    Task<ProdutoDetalhesDto?> ObterDetalhesProdutoAsync(int produtoId);
}
