using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using MyAspNetCore.Web.Filters;
using MyAspNetCore.Web.Helpers;
using MyAspNetCore.Web.Models;
using MyAspNetCore.Web.ViewModel;

namespace MyAspNetCore.Web.Controllers
{
    [Route("[controller]/action")]
    public class ProductsController : Controller
    {
        private AppDbContext _context;
        private readonly IFileProvider _fileProvider;
        private readonly IMapper _mapper;       
        private readonly ProductRepository _productRepository;

        public ProductsController(AppDbContext context, IMapper mapper, IFileProvider fileProvider)
        {
            _productRepository = new ProductRepository();
            _context = context;
            _mapper = mapper;
            _fileProvider = fileProvider;
        }

        //[CacheResourceFilter]
        public IActionResult Index()
        {
            List<ProductViewModel> products = _context.Products.Include(x => x.Category).Select(x => new ProductViewModel()
            {
                Id= x.Id,
                Name= x.Name,
                Price= x.Price,
                Stock= x.Stock,
                CategoryName=x.Category.Name,
                Color=x.Color,
                Description=x.Description,
                Expire = x.Expire,
                ImagePath=x.ImagePath,
                IsPublish=x.IsPublish,
                PublishDate=x.PublishDate

            }).ToList();


            return View(products);
        }

        // [HttpGet("{page}/{pageSize}")]
        [Route("[controller]/[action]/{page}/{pageSize}", Name = "productpage")]
        public IActionResult Pages(int page,int pageSize)
        {
            var products=_context.Products.Skip((page-1)*pageSize).Take(pageSize).ToList();

            ViewBag.page=page;
            ViewBag.pageSize=pageSize;

            return View(_mapper.Map<List<ProductViewModel>>(products));
        }

        [ServiceFilter(typeof(NotFoundFilter))]
        [Route("urunler/urun/{productid}", Name = "product")]
        public IActionResult GetById(int productid)
        {
            var product = _context.Products.Find(productid);

            return View(_mapper.Map<ProductViewModel>(product));
        }
        
        [ServiceFilter(typeof(NotFoundFilter))]
        [HttpGet("{id}")]
        public IActionResult Remove(int id)
        {
            var product = _context.Products.Find(id);

            _context.Products.Remove(product);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        
        
        [HttpGet]
        //get sayfayi gostermek icin kullanilir
        public IActionResult Add()
        {
            ViewBag.Expire = new Dictionary<string, int>()
            {
                {"1.Ay",1 },
                {"3.Ay",3 },
                {"6.Ay",6 },
                {"12.Ay",12},
                //key e ve value ye karsilik gelir
            };



            ViewBag.ColorSelect = new SelectList(new List<ColorSelectList>()
            {
                new(){Data="Mavi",Value="Mavi"},
                new(){Data="Kirmizi",Value="Kirmizi"},
                new(){Data="Sari",Value="Sari"}
            }, "Value","Data");
            
            var categories=_context.Category.ToList();

            ViewBag.categorySelect=new SelectList(categories,"Id","Name");
            

            return View();
        }




        //genellikle postlerin sayfasi olmaz postlarin sayfasi olur

        //kullanici butona bastiginda calisir
        [HttpPost]
        public IActionResult AddProduct(ProductViewModel newProduct)
        {
            IActionResult result = null;




            if (ModelState.IsValid)
            {
                try
                {
                    var product = _mapper.Map<Product>(newProduct);
                    if (newProduct.Image != null && newProduct.Image.Length > 0)
                    {
                        var root = _fileProvider.GetDirectoryContents("wwwroot");

                        var images = root.First(x => x.Name == "images");


                        var randomImageName = Guid.NewGuid() + Path.GetExtension(newProduct.Image.FileName);


                        var path = Path.Combine(images.PhysicalPath, randomImageName);


                        using var stream = new FileStream(path, FileMode.Create);


                        newProduct.Image.CopyTo(stream);

                        product.ImagePath = randomImageName;
                    }







                    _context.Products.Add(product);
                    _context.SaveChanges();

                    TempData["status"] = "Ürün başarıyla eklendi.";
                    return RedirectToAction("Index");

                }
                catch (Exception)
                {

                    result = View();
                }
            }
            else
            {
                result = View();
            }

           // var categories = _context.Category.ToList();

           // ViewBag.categorySelect = new SelectList(categories, "Id", "Name");


            ViewBag.Expire = new Dictionary<string, int>()
            {
                { "1 Ay",1 },
                 { "3 Ay",3 },
                 { "6 Ay",6 },
                 { "12 Ay",12 }
            };

            ViewBag.ColorSelect = new SelectList(new List<ColorSelectList>() {

                new(){ Data="Mavi" ,Value="Mavi" },
                  new(){ Data="Kırmızı" ,Value="Kırmızı" },
                    new(){ Data="Sarı" ,Value="Sarı" }


            }, "Value", "Data");

            return result;


        }

        [ServiceFilter(typeof(NotFoundFilter))]
        [HttpGet]
        public IActionResult Update(int id) 
        {
            var product= _context.Products.Find(id);
            var categories = _context.Category.ToList();

            ViewBag.categorySelect = new SelectList(categories, "Id", "Name",product.CategoryId);

            ViewBag.ExpireValue =  product.Expire;
            ViewBag.Expire = new Dictionary<string, int>()
            {
                {"1.Ay",1 },
                {"3.Ay",3 },
                {"6.Ay",6 },
                {"12.Ay",12},
                //key e ve value ye karsilik gelir
            };

            ViewBag.ColorSelect = new SelectList(new List<ColorSelectList>()
            {
                new(){Data="Mavi",Value="Mavi"},
                new(){Data="Kirmizi",Value="Kirmizi"},
                new(){Data="Sari",Value="Sari"}
            }, "Value", "Data",product.Color);
           
            return View(_mapper.Map<ProductUpdateViewModel>(product));
            //guncellenicek ogenin dolu gelmesini saglar 
        }

        [HttpPost]
        public IActionResult Update(ProductUpdateViewModel updateProduct)
        {
            
         
            if (!ModelState.IsValid)
            {
                ViewBag.ExpireValue = updateProduct.Expire;
                ViewBag.Expire = new Dictionary<string, int>()
            {
                {"1.Ay",1 },
                {"3.Ay",3 },
                {"6.Ay",6 },
                {"12.Ay",12},
                //key e ve value ye karsilik gelir
            };

                ViewBag.ColorSelect = new SelectList(new List<ColorSelectList>()
            {
                new(){Data="Mavi",Value="Mavi"},
                new(){Data="Kirmizi",Value="Kirmizi"},
                new(){Data="Sari",Value="Sari"}
            }, "Value", "Data", updateProduct.Color);

                var categories = _context.Category.ToList();

                ViewBag.categorySelect = new SelectList(categories, "Id", "Name",updateProduct.CategoryId);

                return View();   
            }

            if (updateProduct.Image != null && updateProduct.Image.Length > 0)
            {
                var root = _fileProvider.GetDirectoryContents("wwwroot");

                var images = root.First(x => x.Name == "images");


                var randomImageName = Guid.NewGuid() + Path.GetExtension(updateProduct.Image.FileName);


                var path = Path.Combine(images.PhysicalPath, randomImageName);


                using var stream = new FileStream(path, FileMode.Create);


                updateProduct.Image.CopyTo(stream);

                updateProduct.ImagePath = randomImageName;
            }



            _context.Products.Update(_mapper.Map<Product>(updateProduct));
            _context.SaveChanges();
            TempData["status"] = "urun basayirla guncellendi";
            return RedirectToAction("Index");
        }

        [AcceptVerbs("GET","POST")]// GET veya post olablilir
        public IActionResult HasProductName(string name)
            //urun ismi var mi yok mu diye kontrol eder
        {
            var anyProduct = _context.Products.Any(x => x.Name.ToLower() == name.ToLower());

            if(anyProduct)
            {
                return Json("urun ismi veri tabaninda bulunmaktadir");
            }
            else
            {
                return Json(true);
            }
        }
    }
}
