using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace L5T2UnitOfWork.Model
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public virtual ICollection<ProductCategory> ProductCategories { get; set; } = new List<ProductCategory>();
    }
}
