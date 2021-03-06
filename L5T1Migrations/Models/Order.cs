using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace L5T1Migrations.Models
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
