using ECommerceBack.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerceBack.Infra.Database.EntityTypeConfigurations;

public class ItemEntityTypeConfiguration : IEntityTypeConfiguration<Item>
{
    public void Configure(EntityTypeBuilder<Item> builder)
    {
        builder.Navigation(i => i.Tamanho).AutoInclude();
        builder.Navigation(i => i.Cor).AutoInclude();
    }
}
