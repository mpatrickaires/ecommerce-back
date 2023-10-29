using ECommerceBack.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ECommerceBack.Infra.Context;

public class ECommerceDbContext : DbContext
{
    public ECommerceDbContext(DbContextOptions<ECommerceDbContext> options) : base(options)
    {
    }

    public DbSet<Usuario> Usuarios { get; set; }
}
