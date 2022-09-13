using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PagedList.Core;
using webmvcEF.DTO;


namespace webmvcEF.Controllers
{
    public class ProductListViewController : Controller
    {
        private readonly OnlineShopContext _context;

        public ProductListViewController(OnlineShopContext context)
        {
            _context = context;
        }
        public IActionResult Index(int CatID)
        {

            ViewData["DanhMuc"] = new SelectList(_context.Categories, "Id", "CatName", CatID);
            return View();
        }

        [HttpGet]
        public IActionResult ListProductView(int CatID)
        {
            List<ProductListView> list = new List<ProductListView>();

            if (CatID != 0)
            {
                list = _context.ProductListViews.Where(x => x.CategoryId == CatID).ToList();
            }
            else
            {
                list = _context.ProductListViews.ToList();
            }
            return PartialView("View", list);
        }

    }
}
