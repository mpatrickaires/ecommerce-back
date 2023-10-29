using AutoMapper;
using ECommerceBack.Application.Dtos;
using ECommerceBack.Application.Services.Interfaces;
using ECommerceBack.Common;
using ECommerceBack.Domain.Entities;
using ECommerceBack.Domain.Repositories;

namespace ECommerceBack.Application.Services;

public class UsuarioService : IUsuarioService
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly NotificationContext _notificationContext;
    private readonly IMapper _mapper;

    public UsuarioService(IUsuarioRepository usuarioRepository, NotificationContext notificationContext, IMapper mapper)
    {
        _usuarioRepository = usuarioRepository;
        _notificationContext = notificationContext;
        _mapper = mapper;
    }

    public async Task CadastrarUsuarioAsync(CadastroUsuarioDto cadastroUsuario)
    {
        if (await _usuarioRepository.BuscarPorExpressaoAsync(u => u.Email == cadastroUsuario.Email) != null)
        {
            _notificationContext.AdicionarNotificacao("E-mail já em uso.");
            return;
        }

        Usuario usuario = _mapper.Map<Usuario>(cadastroUsuario);

        if (usuario.EhInvalido)
        {
            _notificationContext.AdicionarNotificacoes(usuario.ValidationResult);
            return;
        }

        _usuarioRepository.Inserir(usuario);
        await _usuarioRepository.SalvarAsync();
    }
}
