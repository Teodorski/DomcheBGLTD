using DomcheBGLTD.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using DomcheBGLTD.Models;

namespace DomcheBGLTD.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        // #1  Homepage (map + testimonials prototype)
        public IActionResult Index() => View();

        // #4  “For Sale / For Rent” selection page
        public IActionResult Select() => View();

        // #5  Static About Us page
        public IActionResult About() => View();

        // #6  Support / FAQ (hard-coded items)
        public IActionResult Support()
        {
            var faqs = FaqItem.Seed();          // helper in ViewModels/FaqItem.cs
            return View(faqs);
        }

        // default template pages still useful
        public IActionResult Privacy() => View();
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
            => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}