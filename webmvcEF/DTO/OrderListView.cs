using System;
using System.Collections.Generic;

namespace webmvcEF.DTO
{
    public partial class OrderListView
    {
        public int Id { get; set; }
        public int? CustomerId { get; set; }
        public string? FullName { get; set; }
        public string? ProductName { get; set; }
        public string? Supplier { get; set; }
        public bool? Payment { get; set; }
    }
}
