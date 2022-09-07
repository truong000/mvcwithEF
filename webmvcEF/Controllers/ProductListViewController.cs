using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
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
        public IActionResult Index(int cat)
        {
            ViewData["Danhmuc"] = new SelectList(_context.Categories, "Id", "CatName", cat);
            return View();
        }

        [HttpGet]
        public IActionResult ListProductView(int CatID)
        {
            var list = _context.ProductListViews.ToList();
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

        //public IActionResult Filter(int CatID = 0)
        //{
        //    var url = $"/ProductListView?CatID={CatID}";
        //    if (CatID == 0)
        //    {
        //        url = $"/ProductListView";
        //    }
        //    return Json(new { status = "success", redirectUrl = url });
        //}


        //[HttpGet]
        //public IActionResult GetData(string searchCategory)
        //{
        //    List<ProductListView> results = null;
        //    if (!string.IsNullOrEmpty(searchCategory))
        //    {
        //        results = _context.ProductListViews.Where(x => x.CatName.ToLower().Contains(searchCategory.Trim().ToLower())).ToList();
        //    }
        //    else
        //    {
        //        results = _context.ProductListViews.ToList();
        //    }
        //    return Json(new
        //    {
        //        Data = results,
        //    });
        //}
    }
}
