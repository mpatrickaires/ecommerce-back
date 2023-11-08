using ECommerceBack.Domain.Entities;
using ECommerceBack.Domain.Repositories;
using ECommerceBack.Infra.Database;

namespace ECommerceBack.Infra.Repositories;

public class ProdutoRepository : RepositoryBase<Produto>, IProdutoRepository
{
    public ProdutoRepository(ECommerceDbContext context) : base(context)
    {
    }
}
