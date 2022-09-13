namespace webmvcEF.Models
{
    public class ProductListView
    {
        public int? Id { get; set; }
        public string? ProductName { get; set; }
        public decimal? Price { get; set; }
        public string? Supplier { get; set; }
        public string? CatName { get; set; }
        public int? CategoryId { get; set; }
    }
}
