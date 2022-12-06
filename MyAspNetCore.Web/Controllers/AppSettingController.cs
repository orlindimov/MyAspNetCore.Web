using Microsoft.AspNetCore.Mvc;

namespace MyAspNetCore.Web.Controllers
{

    public class AppSettingController : Controller
    {
        private readonly IConfiguration _configuration;

        public AppSettingController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        //appsettingjson dan okumayi saglar

        public IActionResult Index()
        {
            ViewBag.baseUrl = _configuration["baseUrl"];
            ViewBag.smsKey = _configuration["Keys:Sms"];

            return View();
        }
    }
}
