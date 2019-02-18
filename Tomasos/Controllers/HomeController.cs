using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Tomasos.Models;

namespace Tomasos.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index(string result)
        {
            if (result == "Success")
            {
                ViewData["Result"] = "Success!";
            }
            else if (result == "Failed")
            {
                ViewData["Result"] = "Failed";
            }
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
