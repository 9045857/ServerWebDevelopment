using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace L4T1ShopEF.Model
{
    internal class Category
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public List<ProductCategory> ProductCategories { get; set; } = new List<ProductCategory>();
    }
}
