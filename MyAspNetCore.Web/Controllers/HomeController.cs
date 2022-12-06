using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MyAspNetCore.Web.Filters;
using MyAspNetCore.Web.Models;
using MyAspNetCore.Web.ViewModel;
using System.Diagnostics;
using System.Security.Cryptography.Xml;

namespace MyAspNetCore.Web.Controllers
{
    [Route("[controller]/action")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public HomeController(ILogger<HomeController> logger, AppDbContext context, IMapper mapper)
        {
            _logger = logger;
            _context = context;
            _mapper = mapper;
        }
        [Route("/")]
        [Route("/Home")]
        [Route("/Home/Index")]
        public IActionResult Index()
        {
            //ordebydescending =id sine gore sondan itibaren siralar
            var products = _context.Products.OrderByDescending(x => x.Id).Select(x => new ProductPartialViewModel()
            {
                Id = x.Id,
                Name = x.Name,
                Price = x.Price,
                Stock = x.Stock
            }).ToList();

            ViewBag.productListPartialViewModel = new ProductListPartialViewModel()
            {
                Products = products
            };
            return View();
        }

        [CustomExceptionFilter]
        public IActionResult Privacy()
        {
            throw new Exception("veritabani hatasi");

            var products = _context.Products.OrderByDescending(x => x.Id).Select(x => new ProductPartialViewModel()
            {
                Id = x.Id,
                Name = x.Name,
                Price = x.Price,
                Stock = x.Stock
            }).ToList();

            ViewBag.productListPartialViewModel = new ProductListPartialViewModel()
            {
                Products = products
            };
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(ErrorViewModel errorViewModel)
        {
            errorViewModel.RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            //traceidentifier =uniq bir id atar

            return View(errorViewModel);
        }

        public IActionResult Visitor()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SaveVisitorComment(VIsitorViewModel vIsitorViewModel)
        {
            try
            {

                var visitor = _mapper.Map<Visitor>(vIsitorViewModel);

                visitor.Created = DateTime.Now;

                _context.Visitors.Add(visitor);

                _context.SaveChanges();

                TempData["result"] = "yorum kaydedilmistir";

                return RedirectToAction(nameof(HomeController.Visitor));
            }
            catch (Exception)
            {
                TempData["result"] = "yorum kaydedilirken hata olustu";

                return RedirectToAction(nameof(HomeController.Visitor));
            }
        }
    }
}