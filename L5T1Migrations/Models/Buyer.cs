using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace L5T1Migrations.Models
{
    public class Buyer
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        //private DateTime _birthday;

        public DateTime? Birthday { get; set; }
        //{
        //    get => _birthday.Date;
        //    set => _birthday = value;
        //}

        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
