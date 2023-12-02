using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerceBack.Domain.Entities;

public class Produto : Entity
{
    public string Nome { get; set; }
    public string Descricao { get; set; }
    public decimal Preco { get; set; }
    public List<ProdutoImagem> Imagens { get; set; } = new();
    [NotMapped]
    public List<ProdutoImagem> ImagensOrdenadas => Imagens.OrderBy(i => i.Ordem).ToList();
    [NotMapped]
    public ProdutoImagem ImagemPrincipal => ImagensOrdenadas.First();
    public List<Item> Itens { get; set; } = new();
}
