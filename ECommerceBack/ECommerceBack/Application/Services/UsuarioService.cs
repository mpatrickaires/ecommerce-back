using AutoMapper;
using ECommerceBack.Application.Dtos;
using ECommerceBack.Application.Services.Interfaces;
using ECommerceBack.Common;
using ECommerceBack.Common.Options;
using ECommerceBack.Domain.Entities;
using ECommerceBack.Domain.Repositories;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace ECommerceBack.Application.Services;

public class UsuarioService : IUsuarioService
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly NotificationContext _notificationContext;
    private readonly IMapper _mapper;
    private readonly JwtOptions _jwtOptions;

    public UsuarioService(IUsuarioRepository usuarioRepository, NotificationContext notificationContext, IMapper mapper, 
        IOptions<JwtOptions> jwtOptions)
    {
        _usuarioRepository = usuarioRepository;
        _notificationContext = notificationContext;
        _mapper = mapper;
        _jwtOptions = jwtOptions.Value;
    }

    public async Task CadastrarUsuarioAsync(CadastroUsuarioDto cadastroUsuario)
    {
        if (await _usuarioRepository.BuscarPorEmailAsync(cadastroUsuario.Email) != null)
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

        usuario.Senha = GerarHashSenha(usuario.Senha);

        _usuarioRepository.Inserir(usuario);
        await _usuarioRepository.SalvarAsync();
    }

    private string GerarHashSenha(string senha)
    {
        StringBuilder stringBuilder = new StringBuilder();

        using SHA256 sha256 = SHA256.Create();
        foreach (byte b in sha256.ComputeHash(Encoding.UTF8.GetBytes(senha)))
        {
            stringBuilder.Append(b.ToString("x2"));
        }

        return stringBuilder.ToString();
    }

    public async Task<TokenDto?> LoginAsync(CredenciaisLoginDto credenciais)
    {
        Usuario? usuario = await _usuarioRepository.BuscarPorEmailAsync(credenciais.Email);
        if (usuario == null)
        {
            _notificationContext.AdicionarNotificacao("Usuário não encontrado para o e-mail informado.");
            return null;
        }

        if (usuario.Senha != GerarHashSenha(credenciais.Senha))
        {
            _notificationContext.AdicionarNotificacao("Senha inválida.");
            return null;
        }

        return GerarToken(usuario);
    }

    public TokenDto GerarToken(Usuario usuario)
    {
        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SecretKey)), SecurityAlgorithms.HmacSha256);

        DateTime dataAtual = DateTime.UtcNow;
        DateTime dataExpiracao = dataAtual.AddDays(1);
        
        var token = new JwtSecurityToken(
            _jwtOptions.Issuer,
            _jwtOptions.Audience,
            GerarClaims(usuario),
            null,
            dataExpiracao,
            signingCredentials);

        return new TokenDto(new JwtSecurityTokenHandler().WriteToken(token), (dataExpiracao - dataAtual).TotalMilliseconds);
    }

    private Claim[] GerarClaims(Usuario usuario) => new Claim[]
    {
        new(JwtRegisteredClaimNames.Sub, usuario.Id.ToString()),
        new(JwtRegisteredClaimNames.Email, usuario.Email),
        new(JwtRegisteredClaimNames.Name, usuario.Nome),
    };
}
