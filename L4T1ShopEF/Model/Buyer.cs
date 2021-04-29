using System.Collections.Generic;

namespace L4T1ShopEF.Model
{
    public class Buyer
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
