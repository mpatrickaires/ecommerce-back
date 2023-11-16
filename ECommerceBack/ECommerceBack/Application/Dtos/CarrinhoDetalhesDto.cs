namespace ECommerceBack.Application.Dtos;

public class CarrinhoDetalhesDto
{
    public IEnumerable<CarrinhoDetalhesItemDto> Itens { get; set; }
    public decimal PrecoTotal => Itens.Sum(x => x.PrecoTotal);
}

public class CarrinhoDetalhesItemDto
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Tamanho { get; set; }
    public string Cor { get; set; }
    public int Quantidade { get; set; }
    public decimal PrecoUnitario { get; set; }
    public decimal PrecoTotal => Quantidade * PrecoUnitario;
}
