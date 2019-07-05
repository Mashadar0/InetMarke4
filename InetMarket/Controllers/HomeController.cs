using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using InetMarket.Models;
using Microsoft.AspNetCore.Authorization;

namespace InetMarket.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        [AllowAnonymous]
        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                //return Content(User.Identity.Name);
                return View();
            }
            return Content("не аутентифицирован");
        }
        public IActionResult About()
        {
            return Content("Authorized");
        }

        /*public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }*/

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
