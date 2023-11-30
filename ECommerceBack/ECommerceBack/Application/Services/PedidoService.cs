using ECommerceBack.Application.Authentication;
using ECommerceBack.Application.Services.Interfaces;
using ECommerceBack.Common.Notification;
using ECommerceBack.Domain.Entities;
using ECommerceBack.Domain.Repositories;

namespace ECommerceBack.Application.Services;

public class PedidoService : IPedidoService
{
    private readonly IPedidoRepository _pedidoRepository;
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly ICarrinhoRepository _carrinhoRepository;
    private readonly IUsuarioLogado _usuarioLogado;
    private readonly NotificationContext _notificationContext;

    public PedidoService(IPedidoRepository pedidoRepository, IUsuarioRepository usuarioRepository, ICarrinhoRepository carrinhoRepository, IUsuarioLogado usuarioLogado, NotificationContext notificationContext)
    {
        _pedidoRepository = pedidoRepository;
        _usuarioRepository = usuarioRepository;
        _carrinhoRepository = carrinhoRepository;
        _usuarioLogado = usuarioLogado;
        _notificationContext = notificationContext;
    }

    public async Task CriarPedidoAsync()
    {
        Usuario? usuario = await _usuarioRepository.BuscarPorIdAsync(_usuarioLogado.Id);
        if (usuario == null)
        {
            _notificationContext.AdicionarNotificacao(NotificationMessages.UsuarioNaoEncontrado);
            return;
        }

        IEnumerable<CarrinhoItem> carrinhoItens = await _carrinhoRepository.BuscarCarrinhoItensAsync(_usuarioLogado.Id);
        if (!carrinhoItens.Any())
        {
            _notificationContext.AdicionarNotificacao("Não é possível criar um pedido pois o carrinho está vazio.");
            return;
        }

        foreach (CarrinhoItem carrinhoItem in carrinhoItens)
        {
            Item item = carrinhoItem.Item;
            string produtoDescricao = item.Produto.Descricao;

            if (!item.EstaDisponivel)
            {
                _notificationContext.AdicionarNotificacao($"O item selecionado para o produto {produtoDescricao} não está mais disponível.");
                continue;
            }
            if (carrinhoItem.QuantidadeItem > item.QuantidadeEstoque)
            {
                _notificationContext.AdicionarNotificacao($"Quantidade insuficiente do item do produto {produtoDescricao} em estoque. Quantidade máxima: {item.QuantidadeEstoque}");
            }
        }

        if (_notificationContext.PossuiNotificacoes)
        {
            return;
        }

        foreach (CarrinhoItem carrinhoItem in carrinhoItens)
        {
            carrinhoItem.Item.QuantidadeEstoque -= carrinhoItem.QuantidadeItem;
            _carrinhoRepository.Deletar(carrinhoItem);
        }

        var pedido = new Pedido
        {
            Usuario = usuario,
            DataPedido = DateTime.UtcNow,
            Itens = carrinhoItens.Select(i => new PedidoItem
            {
                Item = i.Item,
                QuantidadeItem = i.QuantidadeItem,
                PrecoUnitarioItem = i.Item.Produto.Preco,
            }).ToList(),
        };
        _pedidoRepository.Inserir(pedido);
        await _pedidoRepository.SalvarAsync();
    }
}
