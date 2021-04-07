using System.ComponentModel.DataAnnotations;

namespace L4T1ShopEF.Model
{
    internal class ProductCategory
    {
        [Key]
        public int Id { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
