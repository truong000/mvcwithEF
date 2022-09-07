using System;
using System.Collections.Generic;

namespace webmvcEF.DTO
{
    public partial class Order
    {
        public Order()
        {
            OrderProducts = new HashSet<OrderProduct>();
        }

        public int Id { get; set; }
        public int? CustomerId { get; set; }
        public bool? Payment { get; set; }

        public virtual Customer? Customer { get; set; }
        public virtual ICollection<OrderProduct> OrderProducts { get; set; }
    }
}
