using ECommerceBack.Application.Authentication;
using ECommerceBack.Application.Services.Interfaces;
using ECommerceBack.Common;
using ECommerceBack.Domain.Entities;
using ECommerceBack.Domain.Repositories;

namespace ECommerceBack.Application.Services;

public class CarrinhoService : ICarrinhoService
{
    private readonly ICarrinhoRepository _carrinhoRepository;
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IItemRepository _itemRepository;
    private readonly IUsuarioLogado _usuarioLogado;
    private readonly NotificationContext _notificationContext;

    public CarrinhoService(ICarrinhoRepository carrinhoRepository, IUsuarioRepository usuarioRepository, IItemRepository itemRepository, 
        IUsuarioLogado usuarioLogado, NotificationContext notificationContext)
    {
        _carrinhoRepository = carrinhoRepository;
        _usuarioRepository = usuarioRepository;
        _itemRepository = itemRepository;
        _usuarioLogado = usuarioLogado;
        _notificationContext = notificationContext;
    }

    public async Task AdicionarItemAoCarrinhoAsync(int itemId, int quantidade = 1)
    {
        Usuario? usuario = await _usuarioRepository.BuscarPorIdAsync(_usuarioLogado.Id);
        if (usuario == null)
        {
            _notificationContext.AdicionarNotificacao("Usuário não encontrado.");
            return;
        }

        Item? item = await _itemRepository.BuscarPorIdAsync(itemId);
        if (item == null)
        {
            _notificationContext.AdicionarNotificacao("Item não encontrado.");
            return;
        }
        if (!item.EstaDisponivel)
        {
            _notificationContext.AdicionarNotificacao("Esse item não está disponível.");
            return;
        }
        if (item.QuantidadeEstoque < quantidade)
        {
            _notificationContext.AdicionarNotificacao($"Quantidade insuficiente do item em estoque. Quantidade máxima: {item.QuantidadeEstoque}.");
            return;
        }

        CarrinhoItem? carrinhoItem = await _carrinhoRepository.BuscarCarrinhoItemAsync(_usuarioLogado.Id, itemId);
        if (carrinhoItem != null)
        {
            _notificationContext.AdicionarNotificacao("Item já adicionado ao carrinho.");
            return;
        }

        _carrinhoRepository.Inserir(new CarrinhoItem
        {
            Usuario = usuario,
            Item = item,
            QuantidadeItem = quantidade,
        });
        await _carrinhoRepository.SalvarAsync();
    }

    public async Task AlterarQuantidadeItemNoCarrinhoAsync(int itemId, int quantidade)
    {
        if (quantidade < 0)
        {
            _notificationContext.AdicionarNotificacao("A quantidade não pode ser menor que zero.");
            return;
        }

        Usuario? usuario = await _usuarioRepository.BuscarPorIdAsync(_usuarioLogado.Id);
        if (usuario == null)
        {
            _notificationContext.AdicionarNotificacao("Usuário não encontrado.");
            return;
        }

        Item? item = await _itemRepository.BuscarPorIdAsync(itemId);
        if (item == null)
        {
            _notificationContext.AdicionarNotificacao("Item não encontrado.");
            return;
        }

        CarrinhoItem? carrinhoItem = await _carrinhoRepository.BuscarCarrinhoItemAsync(_usuarioLogado.Id, itemId);
        if (carrinhoItem == null)
        {
            _notificationContext.AdicionarNotificacao("O carrinho não possui o item especificado.");
            return;
        }

        if (!item.EstaDisponivel)
        {
            _notificationContext.AdicionarNotificacao("Esse item não está disponível.");
            return;
        }
        if (item.QuantidadeEstoque < quantidade)
        {
            _notificationContext.AdicionarNotificacao($"Quantidade insuficiente do item em estoque. Quantidade máxima: {item.QuantidadeEstoque}.");
            return;
        }

        if (quantidade == 0)
        {
            _carrinhoRepository.Deletar(carrinhoItem);
        }
        else
        {
            carrinhoItem.QuantidadeItem = quantidade;
        }
        await _carrinhoRepository.SalvarAsync();
    }

    public Task RemoverItemDoCarrinhoAsync(int itemId) => AlterarQuantidadeItemNoCarrinhoAsync(itemId, 0);
}
