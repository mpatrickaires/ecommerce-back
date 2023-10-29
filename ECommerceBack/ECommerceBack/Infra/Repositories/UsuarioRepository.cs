using ECommerceBack.Domain.Entities;
using ECommerceBack.Domain.Repositories;
using ECommerceBack.Infra.Context;
using System.Security.Cryptography;
using System.Text;

namespace ECommerceBack.Infra.Repositories;

public class UsuarioRepository : RepositoryBase<Usuario>, IUsuarioRepository
{
    public UsuarioRepository(ECommerceDbContext context) : base(context)
    {
    }

    public override void Inserir(Usuario entity)
    {
        entity.Senha = GerarHashSenha(entity.Senha);
        base.Inserir(entity);
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
}
