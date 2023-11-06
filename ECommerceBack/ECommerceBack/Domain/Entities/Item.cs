using Microsoft.EntityFrameworkCore;

namespace ECommerceBack.Domain.Entities;

[Index(nameof(ProdutoId), nameof(TamanhoId), nameof(CorId), IsUnique = true)]
public class Item : Entity
{
    public int ProdutoId { get; set; }
    public Produto Produto { get; set; }
    public int TamanhoId { get; set; }
    public Tamanho Tamanho { get; set; }
    public int CorId { get; set; }
    public Cor Cor { get; set; }
    public int QuantidadeEstoque { get; set; }
}
