using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ECommerceBack.Domain.Entities;

[Index(nameof(Nome), IsUnique = true)]
[Index(nameof(Codigo), IsUnique = true)]
public class Cor : Entity
{
    [MaxLength(100)]
    public string Nome { get; set; }
    [MaxLength(6)]
    public string Codigo { get; set; }
}
