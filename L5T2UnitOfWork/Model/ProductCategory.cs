using System.ComponentModel.DataAnnotations;

namespace L5T2UnitOfWork.Model
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
