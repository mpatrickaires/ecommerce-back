namespace ECommerceBack.Application.Dtos;

public class PedidoItemDto
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Imagem { get; set; }
    public string Tamanho { get; set; }
    public CorDto Cor { get; set; }
    public int QuantidadeItem { get; set; }
    public decimal PrecoUnitarioItem { get; set; }
    public decimal PrecoTotal => QuantidadeItem * PrecoUnitarioItem;
}
