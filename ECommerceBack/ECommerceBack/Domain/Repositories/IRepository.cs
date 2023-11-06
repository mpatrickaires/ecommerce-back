using ECommerceBack.Domain.Entities;
using System.Linq.Expressions;

namespace ECommerceBack.Domain.Repositories;

public interface IRepository<TEntity> where TEntity : Entity
{
    Task<TEntity?> BuscarPorIdAsync(int id);
    Task<TEntity?> BuscarPorExpressaoAsync(Expression<Func<TEntity, bool>> expressao);
    void Inserir(TEntity entity);
    Task SalvarAsync();
}
