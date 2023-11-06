using ECommerceBack.Domain.Entities;
using ECommerceBack.Domain.Repositories;
using ECommerceBack.Infra.Database;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ECommerceBack.Infra.Repositories;

public class RepositoryBase<TEntity> : IRepository<TEntity> where TEntity : Entity
{
    protected readonly ECommerceDbContext Context;
    protected DbSet<TEntity> DbSet => Context.Set<TEntity>();

    public RepositoryBase(ECommerceDbContext context)
    {
        Context = context;
    }

    public virtual Task<TEntity?> BuscarPorExpressaoAsync(Expression<Func<TEntity, bool>> expressao)
    {
        return DbSet.FirstOrDefaultAsync(expressao);
    }

    public virtual Task<TEntity?> BuscarPorIdAsync(int id)
    {
        return DbSet.FirstOrDefaultAsync(e => e.Id == id);
    }

    public virtual void Inserir(TEntity entity)
    {
        DbSet.Add(entity);
    }

    public virtual async Task SalvarAsync()
    {
        await Context.SaveChangesAsync();
    }
}
