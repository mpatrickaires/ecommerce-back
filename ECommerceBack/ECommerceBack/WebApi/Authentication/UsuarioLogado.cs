using ECommerceBack.Application.Authentication;
using System.Security.Claims;

namespace ECommerceBack.WebApi.Authentication;

public class UsuarioLogado : IUsuarioLogado
{
    public UsuarioLogado(IHttpContextAccessor httpContextAccessor)
    {
        ClaimsPrincipal claims = httpContextAccessor.HttpContext.User;

        Id = int.Parse(claims.FindFirst(ClaimTypes.NameIdentifier).Value);
        Email = claims.FindFirst(ClaimTypes.Email).Value;
        Nome = claims.FindFirst("name").Value;
    }

    public int Id { get; }

    public string Email { get; }

    public string Nome { get; }
}
