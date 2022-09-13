using System;
using System.Collections.Generic;

namespace webmvcEF.DTO
{
    public partial class Product
    {
        public Product()
        {
            OrderProducts = new HashSet<OrderProduct>();
        }

        public int Id { get; set; }
        public string? ProductName { get; set; }
        public decimal? Price { get; set; }
        public string? Supplier { get; set; }
        public int CategoryId { get; set; }

        public virtual ICollection<OrderProduct> OrderProducts { get; set; }
    }
}
