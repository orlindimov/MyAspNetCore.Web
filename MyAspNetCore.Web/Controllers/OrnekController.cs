using Microsoft.AspNetCore.Mvc;
using MyAspNetCore.Web.Filters;

namespace MyAspNetCore.Web.Controllers
{
    public class Product2
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }

    [CustomResultFilter("x-version","1.0")]
    [Route("[controller]/action")]
    public class OrnekController : Controller
    {
      

        public IActionResult Index()
        {
            var productList = new List<Product2>()
            {
                new(){Id=1,Name="kalem"},
                new(){Id=2,Name="defter"},
                new(){Id=3,Name="Silgi"}
            };



            //ViewBag.name = "mesut";
            //TempData["surname"] = "ozgen";//bir saydadan diger sayfaya veri tasir
            //ViewBag.name = "Asp.net.core";

            //ViewData["age"] = 30;

            //ViewData["names"] = new List<string>() { "ahmet", "mehmet" };

            //ViewBag.person = new { Id = 1, name = "ahmet", age = 23 };          

            return View(productList);
        }
        public IActionResult Index2()
        {
            return View();
        }

        public IActionResult Index3()
        {
            return RedirectToAction("Index");//index sayfasina yonlendirir
           
        }

        public IActionResult ParametreView(int id)
        {
            return RedirectToAction("JsonResultParametre","Ornek",new {id=id});
        }

        public IActionResult JsonResultParametre(int id)
        {
            return Json(new { Id = 1});
        }

        public IActionResult ContentResult()
        {
            return Content("ContentResult");
        }
        public IActionResult JsonResult()
        {
            return Json(new { Id = 1, name = "kalem 1", price = 100 });
        }
    }
}
