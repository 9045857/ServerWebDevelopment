using Microsoft.EntityFrameworkCore;

namespace L4T1ShopEF.Model
{
    public sealed class ShopContext : DbContext
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
                b.HasKey(pc => pc.Id);

                b.HasOne(pc => pc.Category)
                 .WithMany(c => c.ProductCategories)
                 .HasForeignKey(pc => pc.CategoryId);

                b.HasOne(pc => pc.Product)
                 .WithMany(p => p.ProductCategories)
                 .HasForeignKey(pc => pc.ProductId);
            });

            modelBuilder.Entity<ProductOrder>(b =>
            {
                b.HasKey(po => po.Id);

                b.HasOne(po => po.Order)
                 .WithMany(o => o.ProductOrders)
                 .HasForeignKey(po => po.OrderId);

                b.HasOne(po => po.Product)
                 .WithMany(p => p.ProductOrders)
                 .HasForeignKey(po => po.ProductId);
            });

            modelBuilder.Entity<Order>(b =>
            {
                b.HasKey(o => o.Id);

                b.Property(o => o.BoughtOn)
                 .IsRequired();

                b.HasOne(o => o.Buyer)
                 .WithMany(bu => bu.Orders)
                 .HasForeignKey(o => o.BuyerId);
            });

            modelBuilder.Entity<Product>(b =>
            {
                b.HasKey(p => p.Id);

                b.Property(p => p.Name)
                 .IsRequired()
                 .HasMaxLength(100);

                b.Property(p => p.Price)
                 .HasColumnType("decimal(10, 2)");
            });

            modelBuilder.Entity<Buyer>(p =>
            {
                p.HasKey(b => b.Id);

                p.Property(b => b.Name)
                 .IsRequired()
                 .HasMaxLength(100);
            });

            modelBuilder.Entity<Category>(b =>
            {
                b.HasKey(c => c.Id);

                b.Property(c => c.Name)
                 .IsRequired()
                 .HasMaxLength(100);
            });
        }
    }
}
