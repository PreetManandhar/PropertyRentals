using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Rent.Models;
using System.Linq;

namespace Rent.Controllers
{
    public class RegisterController : Controller
    {
        private readonly RentalDb3Context context;

        public RegisterController(RentalDb3Context context)
        {
            this.context = context;
        }

        public IActionResult Register()
        {
            // Check if user is already logged in
            if (HttpContext.Session.GetString("UserSession") != null)
            {
                return RedirectToAction("Home");
            }
            return View();
        }

        [HttpPost]
        public IActionResult Register(User newUser)
        {
            if (ModelState.IsValid) // Assuming you have validation on the User model
            {
                // Check if user already exists
                var existingUser = context.Users.FirstOrDefault(x => x.Email == newUser.Email);
                if (existingUser == null)
                {
                    // If user does not exist, add to database
                    context.Users.Add(newUser);
                    context.SaveChanges();

                    // Create session for the new user
                    HttpContext.Session.SetString("UserSession", newUser.Email);
                    return RedirectToAction("Login");
                }
                else
                {
                    // User already exists, provide feedback
                    ViewBag.Message = "User already exists.";
                    return View();
                }
            }
            else
            {
                // Model validation failed, return to registration view with errors
                return View(newUser);
            }
        }
    }
}
