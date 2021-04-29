using System.ComponentModel.DataAnnotations;

namespace L5T1Migrations.Models
{
    public class ProductCategory
    {
        [Key]
        public int Id { get; set; }

        public int ProductId { get; set; }

        public virtual Product Product { get; set; }

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }
    }
}
