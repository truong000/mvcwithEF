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
using NToastNotify;
using webmvcEF.DTO;
using webmvcEF.Models;

namespace webmvcEF.Controllers
{
    public class AccountCustomerController : Controller
    {
        private readonly OnlineShopContext _context;
        private readonly IToastNotification _toastNotification;

        public AccountCustomerController(OnlineShopContext context, IToastNotification toastNotification)
        {
            _context = context;
            _toastNotification = toastNotification;
        }


        //Sign up account customer : GET
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Resgister()
        {
            return View();
        }
        //Sign up account customer : POST
        [HttpPost]
        public async Task<IActionResult> Resgister(CustomerModelView user, string email)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Customer customer = new Customer
                    {
                        FullName = user.FullName,
                        Gender = user.Gender,
                        Email = user.Email,
                        Address = user.Address,
                        Phone = user.Phone
                    };
                    var Email = _context.Customers.AsNoTracking().SingleOrDefault(x => x.Email.ToLower() == email.ToLower());
                    if (Email != null)
                    {
                        ViewBag.Alert = "Email already exists";
                        return View(user);
                    }
                    else
                    {
                        try
                        {
                            //Đăng nhập thành công -- lưu thời gian đăng nhập lại
                            _context.Add(customer);
                            await _context.SaveChangesAsync();
                            //Lưu Session Mã Khách hàng
                            HttpContext.Session.SetString("FullName", customer.FullName.ToString());
                            var userName = HttpContext.Session.GetString("FullName");
                            var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Name, customer.FullName),
                            new Claim("FullName", customer.FullName.ToString())
                        };
                            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "login");
                            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                            await HttpContext.SignInAsync(claimsPrincipal);
                            _toastNotification.AddSuccessToastMessage("You have successfully registered.Let's experience the website.");
                            return RedirectToAction("Index", "ProductsWithAjax");
                        }
                        catch
                        {
                            return View();
                        }
                    }
                }
                else
                {
                    return View(user);
                }
            }
            catch
            {
                _toastNotification.AddErrorToastMessage("Account registration failed.");
                return View(user);
            }
        }

        //Sign in account customer : GET
        [HttpGet]
        public IActionResult LoginAccount(string returnUrl = null)
        {
            var taikhoanID = HttpContext.Session.GetString("FullName");

            if (taikhoanID != null)
            {
                return RedirectToAction("Resgister", "AccountCustomer");
            }
            return View();
        }

        //Sign in account customer : POST
        [HttpPost]
        public async Task<IActionResult> LoginAccount(CustomerModelView user, string returnUrl = null)
        {
            //var cd = _context.Accounts.ToList();
            var account = _context.Customers
                    .AsNoTracking()
                    .SingleOrDefault(x => x.Email == user.Email);
            if (account == null)
            {
                ViewBag.Error = "An error occurred";
                return View(user);
            }
            string password = user.Phone;
            if (account.Phone != password)
            {
                ViewBag.Error = "Your phone is incorrect";
                return View(user);
            }
            //Lưu Session Mã Khách hàng
            HttpContext.Session.SetString("FullName", account.FullName.ToString());
            var userName = HttpContext.Session.GetString("FullName");
            var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Name, account.FullName),
                            new Claim("FullName", account.FullName.ToString())
                        };
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "login");
            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
            await HttpContext.SignInAsync(claimsPrincipal);
            _toastNotification.AddSuccessToastMessage("Login successful.");
            if (string.IsNullOrEmpty(returnUrl))
            {
                return RedirectToAction("Index", "ProductsWithAjax");
            }
            else
            {
                return Redirect(returnUrl);
            }

        }

        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync();
            HttpContext.Session.Remove("FullName");
            return RedirectToAction("LoginAccount", "AccountCustomer");
        }
    }
}
