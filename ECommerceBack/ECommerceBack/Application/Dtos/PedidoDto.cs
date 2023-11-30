namespace ECommerceBack.Application.Dtos;

public class PedidoDto
{
    public DateTime DataPedido { get; set; }
    public IEnumerable<PedidoItemDto> Itens { get; set; }
    public decimal PrecoTotal => Itens.Sum(i => i.PrecoTotal);
}
