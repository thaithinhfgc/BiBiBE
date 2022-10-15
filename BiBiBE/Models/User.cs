using System;
using System.Collections.Generic;

#nullable disable

namespace BiBiBE.Models
{
    public partial class User
    {
        public User()
        {
            Orders = new HashSet<Order>();
        }

        public int UserId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public int Role { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
