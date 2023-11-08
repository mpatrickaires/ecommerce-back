namespace ECommerceBack.Application.Dtos;

public class ProdutoDetalhesDto
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Descricao { get; set; }
    public decimal Preco { get; set; }
    public IEnumerable<ProdutoImagemDto> Imagens { get; set; }
    public IEnumerable<ItemDetalhesDto> Itens { get; set; }
}

public class ItemDetalhesDto
{
    public string Tamanho { get; set; }
    public IEnumerable<ItemDetalhesCorDto> Cores { get; set; }
    public bool EstaDisponivel => Cores.Any(c => c.EstaDisponivel);
}

public class ItemDetalhesCorDto
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Codigo { get; set; }
    public int QuantidadeEstoque { get; set; }
    public bool EstaDisponivel => QuantidadeEstoque > 0;
}
