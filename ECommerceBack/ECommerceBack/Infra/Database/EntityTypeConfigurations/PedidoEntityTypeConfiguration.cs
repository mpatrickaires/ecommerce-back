using ECommerceBack.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerceBack.Infra.Database.EntityTypeConfigurations;

public class PedidoEntityTypeConfiguration : IEntityTypeConfiguration<Pedido>
{
    public void Configure(EntityTypeBuilder<Pedido> builder)
    {
        builder.Navigation(p => p.Itens).AutoInclude();
    }
}
