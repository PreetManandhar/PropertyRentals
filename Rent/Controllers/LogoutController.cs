using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace Rent.Controllers
{
    public class LogoutController : Controller
    {
        public IActionResult Logout()
        {
            // Clear the user session
            HttpContext.Session.Remove("UserSession");

            // Redirect to login page after logout
            return RedirectToAction("Login", "Login");
        }
    }
}
