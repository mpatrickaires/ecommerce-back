using ECommerceBack.Domain.Entities;
using ECommerceBack.Domain.Repositories;
using ECommerceBack.Infra.Database;

namespace ECommerceBack.Infra.Repositories;

public class UsuarioRepository : RepositoryBase<Usuario>, IUsuarioRepository
{
    public UsuarioRepository(ECommerceDbContext context) : base(context)
    {
    }

    public Task<Usuario?> BuscarPorEmailAsync(string email) => BuscarPorExpressaoAsync(u => u.Email == email);
}
