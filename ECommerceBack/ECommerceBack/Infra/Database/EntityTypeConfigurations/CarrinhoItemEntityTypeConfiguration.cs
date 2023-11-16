using ECommerceBack.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerceBack.Infra.Database.EntityTypeConfigurations;

public class CarrinhoItemEntityTypeConfiguration : IEntityTypeConfiguration<CarrinhoItem>
{
    public void Configure(EntityTypeBuilder<CarrinhoItem> builder)
    {
        builder.Navigation(c => c.Item).AutoInclude();
    }
}
