using ECommerceBack.Application.Dtos;
using ECommerceBack.Application.Services.Interfaces;
using ECommerceBack.WebApi.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceBack.WebApi.Controllers;

[ApiController]
[Route("api/usuarios")]
public class UsuarioController : ControllerBase
{
    private readonly IUsuarioService _usuarioService;

    public UsuarioController(IUsuarioService usuarioService)
    {
        _usuarioService = usuarioService;
    }

    [HttpPost]
    public async Task<RespostaApiDto> CadastrarUsuarioAsync([FromBody] CadastroUsuarioDto cadastroUsuario)
    {
        await _usuarioService.CadastrarUsuarioAsync(cadastroUsuario);
        return new RespostaApiDto("Usuário cadastrado com sucesso.");
    }

    [HttpPost("login")]
    public async Task<RespostaApiDto<TokenDto?>> LoginAsync([FromBody] CredenciaisLoginDto credenciais)
    {
        TokenDto? token = await _usuarioService.LoginAsync(credenciais);
        return new RespostaApiDto<TokenDto?>(token);
    }
}
