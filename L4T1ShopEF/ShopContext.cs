using L4T1ShopEF.Model;
using Microsoft.EntityFrameworkCore;

namespace L4T1ShopEF
{
    internal sealed class ShopContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Buyer> Buyers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<ProductOrder> ProductOrders { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options
                .UseLazyLoadingProxies()
                .UseSqlServer("Server=.;Integrated Security=true;Database=L4Shop;MultipleActiveResultSets=true");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductCategory>(b =>
            {
                b.HasOne(pc => pc.Category)
                    .WithMany(c => c.ProductCategories)
                    .HasForeignKey(pc => pc.CategoryId);

                b.HasOne(pc => pc.Product)
                    .WithMany(p => p.ProductCategories)
                    .HasForeignKey(pc => pc.ProductId);
            });

            modelBuilder.Entity<ProductOrder>(b =>
             {
                 b.HasOne(po => po.Order)
                     .WithMany(o => o.ProductOrders)
                     .HasForeignKey(po => po.OrderId);

                 b.HasOne(po => po.Product)
                     .WithMany(p => p.ProductOrders)
                     .HasForeignKey(po => po.ProductId);
             });
        }
    }
}
