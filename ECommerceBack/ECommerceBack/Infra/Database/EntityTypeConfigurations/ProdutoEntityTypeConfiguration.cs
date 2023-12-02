using ECommerceBack.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerceBack.Infra.Database.EntityTypeConfigurations;

public class ProdutoEntityTypeConfiguration : IEntityTypeConfiguration<Produto>
{
    public void Configure(EntityTypeBuilder<Produto> builder)
    {
        builder.Navigation(p => p.Imagens).AutoInclude();
    }
}
