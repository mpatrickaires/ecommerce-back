namespace ECommerceBack.Application.Dtos;

public class PedidoItemDto
{
    public string Imagem { get; set; }
    public string Tamanho { get; set; }
    public string Cor { get; set; }
    public int QuantidadeItem { get; set; }
    public decimal PrecoUnitarioItem { get; set; }
    public decimal PrecoTotal => QuantidadeItem * PrecoUnitarioItem;
}
