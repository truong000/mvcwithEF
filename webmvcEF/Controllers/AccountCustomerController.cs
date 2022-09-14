using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using webmvcEF.DTO;
using webmvcEF.Models;

namespace webmvcEF.Controllers
{
    public class AccountCustomerController : Controller
    {
        private readonly OnlineShopContext _context;

        public AccountCustomerController(OnlineShopContext context)
        {
            _context = context;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Resgister()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Resgister(CustomerModelView customerView, string Email)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Customer customer = new Customer
                    {
                        FullName = customerView.FullName,
                        Phone = customerView.Phone.Trim().ToLower(),
                        Email = customerView.Email.Trim().ToLower(),
                        Address = customerView.Address,
                    };
                    var kh = _context.Customers.AsNoTracking().SingleOrDefault(x => x.Email.ToLower() == Email.ToLower());
                    if (kh != null)
                    {
                        ViewBag.ErrorMessage = "Email already exists";
                        return View(customerView);
                    }
                    else
                    {
                        try
                        {
                            _context.Add(customer);
                            await _context.SaveChangesAsync();
                            //Lưu Session Mã Khách hàng
                            HttpContext.Session.SetString("Id", customer.Id.ToString());
                            var taikhoanID = HttpContext.Session.GetString("Id");
                            var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Name, customer.FullName),
                            new Claim("Id", customer.Id.ToString())
                        };
                            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "login");
                            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                            await HttpContext.SignInAsync(claimsPrincipal);
                            return RedirectToAction("Index", "ProductsWithAjax");
                        }
                        catch
                        {
                            return RedirectToAction("DangKyTaiKhoan", "Accounts");
                        }
                    }                    
                }
                else
                {
                    return View(customerView);
                }
            }
            catch
            {
                ViewBag.ErrorMessage = "Đăng ký tài khoản thất bại";
                return View(customerView);
            }
        }
    }
}
