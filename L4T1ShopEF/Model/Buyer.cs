using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace L4T1ShopEF.Model
{
    internal class Buyer
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
