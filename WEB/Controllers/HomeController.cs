using Microsoft.AspNetCore.Mvc;
using WEB.Models;
using System.Diagnostics;

namespace WEB.Controllers
{
    public class HomeController : Controller
    {
        // Hlavní stránka
        public IActionResult Index()
        {
            return View();
        }

        // Tréninkový plán
        public IActionResult Plan()
        {
            return View();
        }

        // Reference
        public IActionResult Reference()
        {
            return View();
        }

        // Kontakt
        public IActionResult Kontakt()
        {
            return View();
        }

        // Privacy (původní stránka)
        public IActionResult Privacy()
        {
            return View();
        }

        // Error stránka
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            });
        }
    }
}