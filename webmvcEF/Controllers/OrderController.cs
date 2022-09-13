using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webmvcEF.DTO;

namespace webmvcEF.Controllers
{
    public class OrderController : Controller
    {
        private readonly OnlineShopContext _context;

        public OrderController(OnlineShopContext context)
        {
            _context = context;
        }
        public IActionResult Index(int orderId)
        {
            var orders = _context.Orders
                .Include(x => x.Customer)
                .Include(x => x.OrderProducts)
                .ToList();
            return View(orders);
        }

        public async Task<IActionResult> ShowDetail(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.Customer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            var chitietdonhang = _context.OrderProducts
                .Include(x => x.Product)
                .AsNoTracking()
                .Where(x => x.OrderId == order.Id)
                .ToList();
            ViewBag.ChiTiet = chitietdonhang;
            return View(order);
        }
    }
}
