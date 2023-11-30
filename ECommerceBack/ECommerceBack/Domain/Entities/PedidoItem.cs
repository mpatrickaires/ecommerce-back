using Microsoft.EntityFrameworkCore;

namespace ECommerceBack.Domain.Entities;

[Index(nameof(PedidoId), nameof(ItemId), IsUnique = true)]
public class PedidoItem : Entity
{
    public int PedidoId { get; set; }
    public Pedido Pedido { get; set; }
    public int ItemId { get; set; }
    public Item Item { get; set; }
    public int QuantidadeItem { get; set; }
    public decimal PrecoUnitarioItem { get; set; }
}
