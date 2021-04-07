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
            options.UseSqlServer("Server=.;Integrated Security=true;Database=L4Shop;MultipleActiveResultSets=true");
        }
    }
}
