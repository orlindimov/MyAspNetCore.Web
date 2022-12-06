using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MyAspNetCore.Web.Models;

namespace MyAspNetCore.Web.Filters
{
    public class NotFoundFilter:ActionFilterAttribute
    {
        private readonly AppDbContext _context;

        public NotFoundFilter(AppDbContext context)
        {
            _context = context;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var idValue = context.ActionArguments.Values.First();
            //ActionArguments=parametre (int id)

            var id = (int)idValue;
            //inte donusturur

            var hasProduct=_context.Products.Any(x=> x.Id==id);

            if (hasProduct==false)
            {
                context.Result = new RedirectToActionResult("Error", "Home",new ErrorViewModel()
                { Errors=new List<string>() { $"id({id}) ye sahip urun veritabaninda bulunamamistir"} });
            }
        }
    }
}
