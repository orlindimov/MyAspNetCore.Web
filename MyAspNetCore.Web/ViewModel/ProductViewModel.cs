using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;
using RequiredAttribute = Microsoft.Build.Framework.RequiredAttribute;

namespace MyAspNetCore.Web.ViewModel
{
    public class ProductViewModel
    {
        public int Id { get; set; }

        
        [Remote(action:"HasProductName","Products")]
        public string Name { get; set; }



        public decimal? Price { get; set; }

        
        public int Stock { get; set; }

        public string Description { get; set; }

        public string Color { get; set; }

        public DateTime? PublishDate { get; set; }

        public bool IsPublish { get; set; }

        public int? Expire { get; set; }

        [ValidateNever]
        public IFormFile? Image { get; set; }

        [ValidateNever]
        public string? ImagePath { get; set; }

        public int CategoryId { get; set; }

        public string? CategoryName { get; set; }


    }
}
