using System;
using System.Collections.Generic;

namespace webmvcEF.DTO
{
    public partial class Inventory
    {
        public Inventory()
        {
            Products = new HashSet<Product>();
        }

        public int Id { get; set; }
        public int? Quantity { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
