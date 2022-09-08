using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webmvcEF.DTO;

namespace webmvcEF.Controllers
{
    public class CustomerInforsController : Controller
    {
        private readonly OnlineShopContext _context;
        public CustomerInforsController(OnlineShopContext context)
        {
            _context = context;
        }

        // GET: Customers
        public async Task<IActionResult> Index()
        {
            var customer = _context.Customers.Include(x => x.Orders).ToList();
            return View(customer);
        }
    }
}
