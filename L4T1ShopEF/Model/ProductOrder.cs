using System.ComponentModel.DataAnnotations;

namespace L4T1ShopEF.Model
{
    public class ProductOrder
    {
        [Key]
        public int Id { get; set; }
        public int Count { get; set; }

        public int ProductId { get; set; }
        public virtual Product Product { get; set; }

        public int OrderId { get; set; }
        public virtual Order Order { get; set; }
    }
}
