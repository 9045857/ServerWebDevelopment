using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace L4T1ShopEF.Model
{
    internal class Order
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime BoughtOn { get; set; }

        [Required]
        public int BuyerId { get; set; }

        [Required]
        public Buyer Buyer { get; set; }

        public virtual ICollection<ProductOrder> ProductOrders { get; set; } = new List<ProductOrder>();
    }
}
