using System;
using System.Collections.Generic;

namespace webmvcEF.DTO
{
    public partial class Category
    {
        public Category()
        {
            Products = new HashSet<Product>();
        }

        public int Id { get; set; }
        public string? CatName { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
