using ECommerceBack.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace ECommerceBack.Infra.Database;

public class ECommerceDbContext : DbContext
{
    public ECommerceDbContext(DbContextOptions<ECommerceDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configurando para que o comportamento de deleção não seja cascade, mas sim restrict.
        foreach (var relacao in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
        {
            relacao.DeleteBehavior = DeleteBehavior.Restrict;
        }

        // Aplicando as configurações de todas as classes que implementam a interface IEntityTypeConfiguration no assembly em execução.
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }

    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Produto> Produtos { get; set; }
    public DbSet<ProdutoImagem> ProdutoImagens { get; set; }
    public DbSet<Cor> Cores { get; set; }
    public DbSet<Tamanho> Tamanhos { get; set; }
    public DbSet<Item> Itens { get; set; }
    public DbSet<CarrinhoItem> CarrinhoItens { get; set; }
}
