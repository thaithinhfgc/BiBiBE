using System;
using System.Collections.Generic;

#nullable disable

namespace BiBiBE.Models
{
    public partial class Order
    {
        public Order()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int OrderId { get; set; }
        public int AccountId { get; set; }
        public double Total { get; set; }
        public DateTime Date { get; set; }

        public virtual User Account { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
