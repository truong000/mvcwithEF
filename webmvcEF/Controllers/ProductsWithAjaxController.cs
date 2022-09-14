using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PagedList.Core;
using webmvcEF.DTO;

namespace webmvcEF.Controllers
{
    public class ProductsWithAjaxController : Controller
    {
        private readonly OnlineShopContext _context;

        public ProductsWithAjaxController(OnlineShopContext context)
        {
            _context = context;
        }

        // GET: ProductsWithAjax
        public IActionResult Index(int CatID)
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "CatName");
            return View();
        }

        [HttpGet]
        public IActionResult ListProduct(int CatID)
        {
            List<Product> list = new List<Product>();

            if (CatID != 0)
            {
                list = _context.Products
                    .Include(x => x.Category)
                    .Where(x => x.CategoryId == CatID)
                    .OrderByDescending(x => x.Id)
                    .ToList();
            }
            else
            {
                list = _context.Products
                    .Include(x => x.Category)
                    .OrderByDescending(x => x.Id)
                    .ToList();
            }
            return PartialView("_ViewProduct",list);
        }

        [HttpPost]
        public IActionResult Search(string search)
        {
            var product = _context.Products.Include(x => x.Category).ToList();
            if (!string.IsNullOrEmpty(search))
            {
                product = product.Where(p => p.ProductName.ToLower().Contains(search.ToLower())).ToList();
            }
            return PartialView("_ViewProduct", product);
        }

        // GET: ProductsWithAjax/Create
        public async Task<IActionResult> AddOrEdit(int id = 0)
        {
            if(id == 0)
            {
                ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "CatName");
                return View(new Product());
            }
            else
            {
                var product = await _context.Products.FindAsync(id);
                if (product == null)
                {
                    return NotFound();
                }
                ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "CatName", product.CategoryId);
                return View(product);
            }

        }

        // POST: ProductsWithAjax/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit(int id, [Bind("Id,ProductName,Price,Supplier,CategoryId")] Product product)
        {
            if (ModelState.IsValid)
            {
                //Insert
                if (id == 0)
                {
                    ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "CatName");
                    _context.Add(product);
                    await _context.SaveChangesAsync();

                }
                //Update
                else
                {
                    try
                    {
                        ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "CatName");
                        _context.Update(product);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!ProductExists(product.Id))
                        { return NotFound(); }
                        else
                        { throw; }
                    }
                }
                return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewProduct", _context.Products.Include(x => x.Category).OrderByDescending(x => x.Id).ToList()) });
            }
            return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "AddOrEdit", product) });
        }


        // GET: ProductsWithAjax/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: ProductsWithAjax/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Products == null)
            {
                return Problem("Entity set 'OnlineShopContext.Products'  is null.");
            }
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
            }
            
            await _context.SaveChangesAsync();
            return Json(new { html = Helper.RenderRazorViewToString(this, "_ViewProduct", _context.Products.Include(x => x.Category).ToList()) });
        }

        private bool ProductExists(int id)
        {
          return (_context.Products?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
