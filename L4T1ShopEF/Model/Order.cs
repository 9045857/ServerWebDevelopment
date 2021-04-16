using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace L4T1ShopEF.Model
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime BoughtOn { get; set; }


        public int BuyerId { get; set; }

        [Required]
        public virtual Buyer Buyer { get; set; }

        public virtual ICollection<ProductOrder> ProductOrders { get; set; } = new List<ProductOrder>();
    }
}
