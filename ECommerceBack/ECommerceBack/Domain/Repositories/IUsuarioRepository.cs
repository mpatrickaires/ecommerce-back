using ECommerceBack.Domain.Entities;

namespace ECommerceBack.Domain.Repositories;

public interface IUsuarioRepository : IRepository<Usuario>
{
    Task<Usuario?> BuscarPorEmailAsync(string email);
}
