namespace ECommerceBack.Domain.Entities;

public class Produto : Entity
{
    public string Nome { get; set; }
    public string Descricao { get; set; }
    public decimal Preco { get; set; }
    public List<ProdutoImagem> Imagens { get; set; } = new();
    public List<Item> Itens { get; set; } = new();
}
