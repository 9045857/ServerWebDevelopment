using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace L4T1ShopEF.Model
{
    internal class Product
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
        public decimal Price { get; set; }

        public List<ProductCategory> ProductCategories { get; set; } = new List<ProductCategory>();

        public List<ProductOrder> ProductOrders { get; set; } = new List<ProductOrder>();
    }
}
