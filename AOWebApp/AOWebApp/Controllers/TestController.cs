using Microsoft.AspNetCore.Mvc;
using System.Collections.Immutable;

namespace AOWebApp.Controllers
{
    public class TestController : Controller
    {
        public IActionResult Test2()
        {
            return View();
        }

        //public IActionResult RazorTest()
        //{


        //    var a = 4;
        //    var b = 13;

        //    var sum = a + b;

        //    ViewBag.sum = sum; 
        //    return View();
        //}
        public IActionResult RazorTest(int? id, string name)
        {
            if(id == null)
            {

            
                ViewBag.id = 666;

            }
            else
            {
                ViewBag.id= id;

            }

            if(name == null)
            {
                ViewBag.name = "Kevin";
            }

            else
            {
                ViewBag.name = name;
            }
       
            return View();
        }

    }
}
