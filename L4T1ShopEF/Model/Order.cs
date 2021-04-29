using System;
using System.Collections.Generic;

namespace L4T1ShopEF.Model
{
    public class Order
    {
        public int Id { get; set; }

        public DateTime BoughtOn { get; set; }

        public int BuyerId { get; set; }

        public virtual Buyer Buyer { get; set; }

        public virtual ICollection<ProductOrder> ProductOrders { get; set; } = new List<ProductOrder>();
    }
}
