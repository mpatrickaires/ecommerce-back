﻿using ECommerceBack.Domain.Entities;

namespace ECommerceBack.Domain.Repositories;

public interface IPedidoRepository : IRepository<Pedido>
{
    Task<IEnumerable<Pedido>> BuscarTodosPedidosUsuarioAsync(int usuarioId);
}
