using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace L5T2UnitOfWork.Model
{
    public class Buyer
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
