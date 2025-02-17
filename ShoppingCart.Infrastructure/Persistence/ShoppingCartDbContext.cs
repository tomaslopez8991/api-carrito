using Microsoft.EntityFrameworkCore;
using ShoppingCart.Domain.Entities;

namespace ShoppingCart.Infrastructure.Persistence;

public class ShoppingCartDbContext : DbContext
{
    public ShoppingCartDbContext(DbContextOptions<ShoppingCartDbContext> options) : base(options) { }

    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Carrito> Carritos { get; set; }
    public DbSet<Producto> Productos { get; set; }
    public DbSet<ItemCarrito> ItemsCarrito { get; set; }
    public DbSet<Compra> Compras { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ItemCarrito>()
            .HasOne(i => i.Carrito)
            .WithMany(c => c.Items)
            .HasForeignKey(i => i.CarritoId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<ItemCarrito>()
            .HasOne(i => i.Producto)
            .WithMany()
            .HasForeignKey(i => i.ProductoId)
            .OnDelete(DeleteBehavior.Restrict);

        base.OnModelCreating(modelBuilder);
    }

}
