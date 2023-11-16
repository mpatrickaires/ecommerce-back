using AutoMapper;
using ECommerceBack.Application.Authentication;
using ECommerceBack.Application.Dtos;
using ECommerceBack.Application.Services.Interfaces;
using ECommerceBack.Common.Notification;
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
    private readonly IMapper _mapper;

    public CarrinhoService(ICarrinhoRepository carrinhoRepository, IUsuarioRepository usuarioRepository, IItemRepository itemRepository,
        IUsuarioLogado usuarioLogado, NotificationContext notificationContext, IMapper mapper)
    {
        _carrinhoRepository = carrinhoRepository;
        _usuarioRepository = usuarioRepository;
        _itemRepository = itemRepository;
        _usuarioLogado = usuarioLogado;
        _notificationContext = notificationContext;
        _mapper = mapper;
    }

    public async Task AdicionarItemAoCarrinhoAsync(int itemId, int quantidade = 1)
    {
        Usuario? usuario = await _usuarioRepository.BuscarPorIdAsync(_usuarioLogado.Id);
        if (usuario == null)
        {
            _notificationContext.AdicionarNotificacao(NotificationMessages.UsuarioNaoEncontrado);
            return;
        }

        Item? item = await _itemRepository.BuscarPorIdAsync(itemId);
        if (item == null)
        {
            _notificationContext.AdicionarNotificacao(NotificationMessages.ItemNaoEncontrado);
            return;
        }
        if (!item.EstaValidoParaAdicionarAoCarrinho(quantidade, out string razaoInvalido))
        {
            _notificationContext.AdicionarNotificacao(razaoInvalido);
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
            _notificationContext.AdicionarNotificacao(NotificationMessages.UsuarioNaoEncontrado);
            return;
        }

        Item? item = await _itemRepository.BuscarPorIdAsync(itemId);
        if (item == null)
        {
            _notificationContext.AdicionarNotificacao(NotificationMessages.ItemNaoEncontrado);
            return;
        }

        CarrinhoItem? carrinhoItem = await _carrinhoRepository.BuscarCarrinhoItemAsync(_usuarioLogado.Id, itemId);
        if (carrinhoItem == null)
        {
            _notificationContext.AdicionarNotificacao("O carrinho não possui o item especificado.");
            return;
        }

        if (!item.EstaValidoParaAdicionarAoCarrinho(quantidade, out string razaoInvalido))
        {
            _notificationContext.AdicionarNotificacao(razaoInvalido);
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

    public async Task<CarrinhoDetalhesDto?> ObterDetalhesCarrinhoAsync()
    {
        Usuario? usuario = await _usuarioRepository.BuscarPorIdAsync(_usuarioLogado.Id);
        if (usuario == null)
        {
            _notificationContext.AdicionarNotificacao(NotificationMessages.UsuarioNaoEncontrado);
            return null;
        }

        IEnumerable<CarrinhoItem> carrinhoItems = await _carrinhoRepository
            .BuscarTodosPorExpressaoAsync(c => c.UsuarioId == _usuarioLogado.Id);

        return new CarrinhoDetalhesDto
        {
            Itens = _mapper.Map<IEnumerable<CarrinhoDetalhesItemDto>>(carrinhoItems)
        };
    }

    public async Task<int?> ObterQuantidadeItensCarrinhoAsync()
    {
        Usuario? usuario = await _usuarioRepository.BuscarPorIdAsync(_usuarioLogado.Id);
        if (usuario == null)
        {
            _notificationContext.AdicionarNotificacao(NotificationMessages.UsuarioNaoEncontrado);
            return null;
        }

        IEnumerable<CarrinhoItem> carrinhoItems = await _carrinhoRepository
            .BuscarTodosPorExpressaoAsync(c => c.UsuarioId == _usuarioLogado.Id);

        return carrinhoItems.Sum(c => c.QuantidadeItem);
    }
}
