using Microsoft.EntityFrameworkCore;

namespace ECommerceBack.Domain.Entities;

[Index(nameof(UsuarioId), nameof(ItemId), IsUnique = true)]
public class CarrinhoItem : Entity
{
    public int UsuarioId { get; set; }
    public Usuario Usuario { get; set; }
    public int ItemId { get; set; }
    public Item Item { get; set; }
    public int QuantidadeItem { get; set; }
}
