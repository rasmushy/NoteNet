using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace NoteNet.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var username = User.Identity.IsAuthenticated ? User.Identity.Name : "Guest";
            ViewData["Username"] = username;
            return View();
        }
    }
}
