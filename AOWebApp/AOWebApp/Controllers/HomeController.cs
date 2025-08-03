using System.Diagnostics;
using AOWebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace AOWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [Route("/")]   // localhost/
        [Route("/Home/")]    // localhost/Home/
        public IActionResult Index()
        {
            Console.WriteLine("Hello");

            for(int i = 0; i < 10; i++ )
            {
                Console.WriteLine("Your lucky number was: "+ i);
            }
            return View();
        }

        public IActionResult NewView()
        {
            return View();

        }


        public IActionResult Test(int? id, string text)
        {
            //var id = Request.RouteValues["id"];

           
            ViewBag.id = id;
            ViewBag.text = text;

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
