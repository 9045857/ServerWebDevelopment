using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace L4T1ShopEF.Model
{
    internal class Order
    {
        [Key]
        public int Id { get; set; }

        public DateTime BoughtOn { get; set; }

        public int BuyerId { get; set; }
        public Buyer Buyer { get; set; }

        public List<ProductOrder> ProductOrders { get; set; } = new List<ProductOrder>();
    }
}
