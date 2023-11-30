namespace ECommerceBack.Domain.Entities;

public class Pedido : Entity
{
    public int UsuarioId { get; set; }
    public Usuario Usuario { get; set; }
    public DateTime DataPedido { get; set; }
    public IEnumerable<PedidoItem> Itens { get; set; }
}
