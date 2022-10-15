using System;
using System.Collections.Generic;

#nullable disable

namespace BiBiBE.Models
{
    public partial class OrderDetail
    {
        public int OrderDetailId { get; set; }
        public int ProductId { get; set; }
        public int OrderId { get; set; }
        public double Price { get; set; }
        public int? Quanity { get; set; }

        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }
    }
}
