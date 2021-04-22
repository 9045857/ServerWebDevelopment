using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace L5T2UnitOfWork.Model
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Column(TypeName = "decimal(10, 2)")]
        public decimal? Price { get; set; }

        public virtual ICollection<ProductCategory> ProductCategories { get; set; } = new List<ProductCategory>();

        public virtual ICollection<ProductOrder> ProductOrders { get; set; } = new List<ProductOrder>();
    }
}
