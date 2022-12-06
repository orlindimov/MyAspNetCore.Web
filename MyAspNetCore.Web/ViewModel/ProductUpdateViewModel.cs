using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc;

namespace MyAspNetCore.Web.ViewModel
{
    public class ProductUpdateViewModel
    {
        public int Id { get; set; }

        public int CategoryId { get; set; }

        [Remote(action: "HasProductName", "Products")]
        public string Name { get; set; }



        public decimal? Price { get; set; }


        public int Stock { get; set; }

        public string Description { get; set; }

        public string Color { get; set; }

        public DateTime PublishDate { get; set; }

        public bool IsPublish { get; set; }

        public string Expire { get; set; }

        [ValidateNever]
        public IFormFile? Image { get; set; }

        [ValidateNever]
        public string ImagePath { get; set; }

    }
}
