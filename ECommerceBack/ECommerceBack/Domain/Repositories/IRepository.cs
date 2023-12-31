﻿using ECommerceBack.Domain.Entities;
using System.Linq.Expressions;

namespace ECommerceBack.Domain.Repositories;

public interface IRepository<TEntity> where TEntity : Entity
{
    Task<IEnumerable<TEntity>> BuscarTodosAsync(params Expression<Func<TEntity, object>>[] relacionamentos);
    Task<TEntity?> BuscarPorIdAsync(int id, params Expression<Func<TEntity, object>>[] relacionamentos);
    Task<TEntity?> BuscarPorExpressaoAsync(Expression<Func<TEntity, bool>> expressao);
    Task<IEnumerable<TEntity>> BuscarTodosPorExpressaoAsync(Expression<Func<TEntity, bool>> expressao, 
        params Expression<Func<TEntity, object>>[] relacionamentos);
    void Inserir(TEntity entity);
    void Deletar(TEntity entity);
    Task SalvarAsync();
}
