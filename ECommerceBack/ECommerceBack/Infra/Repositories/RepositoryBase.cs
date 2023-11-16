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

    public async Task<IEnumerable<TEntity>> BuscarTodosAsync(params Expression<Func<TEntity, object>>[] relacionamentos)
    {
        var query = DbSet.AsQueryable();
        AplicarRelacionamentos(ref query, relacionamentos);
        return await query.ToListAsync();
    }

    public virtual Task<TEntity?> BuscarPorExpressaoAsync(Expression<Func<TEntity, bool>> expressao)
    {
        return DbSet.FirstOrDefaultAsync(expressao);
    }

    public virtual async Task<IEnumerable<TEntity>> BuscarTodosPorExpressaoAsync(Expression<Func<TEntity, bool>> expressao, 
        params Expression<Func<TEntity, object>>[] relacionamentos)
    {
        var query = DbSet.Where(expressao);
        AplicarRelacionamentos(ref query, relacionamentos);
        return await query.ToListAsync();
    }

    public virtual Task<TEntity?> BuscarPorIdAsync(int id, params Expression<Func<TEntity, object>>[] relacionamentos)
    {
        var query = DbSet.AsQueryable();
        AplicarRelacionamentos(ref query, relacionamentos);
        return query.FirstOrDefaultAsync(e => e.Id == id);
    }

    public virtual void Inserir(TEntity entity)
    {
        DbSet.Add(entity);
    }

    public virtual void Deletar(TEntity entity)
    {
        DbSet.Remove(entity);
    }

    public virtual async Task SalvarAsync()
    {
        await Context.SaveChangesAsync();
    }

    private void AplicarRelacionamentos(ref IQueryable<TEntity> query, params Expression<Func<TEntity, object>>[] relacionamentos)
    {
        foreach (var relacionamento in relacionamentos)
        {
            query = query.Include(relacionamento);
        }
    }
}
