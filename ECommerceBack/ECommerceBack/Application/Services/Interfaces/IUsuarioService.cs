using ECommerceBack.Application.Dtos;

namespace ECommerceBack.Application.Services.Interfaces;

public interface IUsuarioService
{
    Task CadastrarUsuarioAsync(CadastroUsuarioDto cadastroUsuario);
    Task<TokenDto?> LoginAsync(CredenciaisLoginDto credenciais);
}
