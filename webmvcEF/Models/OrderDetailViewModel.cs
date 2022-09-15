using webmvcEF.DTO;

namespace webmvcEF.Models
{
    public class OrderDetailViewModel
    {
        public Order order { get; set; }
        public List<OrderProduct> orderProducts { get; set; }
    }
}
