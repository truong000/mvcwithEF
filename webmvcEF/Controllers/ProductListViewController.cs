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

        //public IActionResult GetData(int CatID, int? page, int? pageSize)
        //{
        //    List<ProductListView> results = null;
        //    if (CatID != 0)
        //    {
        //        results = _context.ProductListViews.Where(x => x.CategoryId == CatID).ToList();
        //    }
        //    else
        //    {
        //        results = _context.ProductListViews.ToList();
        //    }

        //    var _pageSize = pageSize ?? 2;
        //    var pageIndex = page ?? 1;
        //    var totalPage = results.Count;
        //    var numberPage = Math.Ceiling((float)totalPage / _pageSize);
        //    var start = (pageIndex - 1) * _pageSize;
        //    results = results.Skip(start).Take(_pageSize).ToList();
        //    return Json(new
        //    {
        //        Data = results,
        //        TotalItems = totalPage,
        //        CurrentPage = pageIndex,
        //        NumberPage = numberPage,
        //        PageSize = _pageSize
        //    });
        //}

    }
}
