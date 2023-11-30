using ECommerceBack.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerceBack.Infra.Database.EntityTypeConfigurations;

public class PedidoItemEntityTypeConfiguration : IEntityTypeConfiguration<PedidoItem>
{
    public void Configure(EntityTypeBuilder<PedidoItem> builder)
    {
        builder.Navigation(p => p.Item).AutoInclude();
    }
}
