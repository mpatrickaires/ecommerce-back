using Microsoft.EntityFrameworkCore;

namespace ECommerceBack.Domain.Entities;

[Index(nameof(UrlImagem), IsUnique = true)]
public class ProdutoImagem : Entity
{
    public int ProdutoId { get; set; }
    public Produto Produto { get; set; }
    public string UrlImagem { get; set; }
    public int Ordem { get; set; }
}
