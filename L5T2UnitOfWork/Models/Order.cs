using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace L5T2UnitOfWork.Models
{
    public partial class Order
    {
        public Order()
        {
            ProductOrders = new HashSet<ProductOrder>();
        }

        public int Id { get; set; }
        public DateTime BoughtOn { get; set; }
        public int BuyerId { get; set; }

        public virtual Buyer Buyer { get; set; }
        public virtual ICollection<ProductOrder> ProductOrders { get; set; }
    }
}
