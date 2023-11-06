using System.ComponentModel.DataAnnotations;

namespace ECommerceBack.Domain.Entities;

public class Tamanho : Entity
{
    [MaxLength(6)]
    public string Nome { get; set; }
}
