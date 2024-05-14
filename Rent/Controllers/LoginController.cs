using Microsoft.AspNetCore.Mvc;

using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

using Rent.Models;

namespace Rent.Controllers
{
    public class LoginController : Controller
    {
        private readonly RentalDb3Context context;
        public LoginController(RentalDb3Context context)
        {
            this.context = context;
        }

        public IActionResult Login()
        {
            if (HttpContext.Session.GetString("UserSession") != null)
            {
                return RedirectToAction("Home");
            }
            return View();
        }
        [HttpPost]
        public IActionResult Login(User user)
        {
            var myUser = context.Users.FirstOrDefault(x =>
                 x.Email.ToLower() == user.Email.ToLower() &&
                 x.Password == user.Password);

            if (myUser != null)
            {
                HttpContext.Session.SetString("UserSession", myUser.Email);

                // Redirect users based on their role
                var userType = myUser.Type.ToLower(); // Convert to lowercase for comparison
                if (userType == "owner" || userType == "Owner")
                {
                    return RedirectToAction("OwnerDashboard");
                }
                else if (userType == "manager")
                {
                    return RedirectToAction("ManagerDashboard");
                }
                else if (userType == "tenant")
                {
                    return RedirectToAction("TenantDashboard");
                }
            }
            else
            {
                ViewBag.Message = "Login failed. Incorrect credentials.";
            }

            return View();
           

        }


        public IActionResult OwnerDashboard()
        {
            if (HttpContext.Session.GetString("UserSession") != null)
            {
                return View();
            }
            return RedirectToAction("Login");
        }

        public IActionResult ManagerDashboard()
        {
            if (HttpContext.Session.GetString("UserSession") != null)
            {
                return View();
            }
            return RedirectToAction("Login");
        }

        public IActionResult TenantDashboard()
        {
            if (HttpContext.Session.GetString("UserSession") != null)
            {
                return View();
            }
            return RedirectToAction("Login");
        }
    }
}
