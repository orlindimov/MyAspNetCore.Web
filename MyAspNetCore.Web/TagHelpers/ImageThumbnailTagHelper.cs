using Microsoft.AspNetCore.Razor.TagHelpers;

namespace MyAspNetCore.Web.TagHelpers
{
    [HtmlTargetElement("thumbnail")]//custom isim vermemizi saglar
    public class ImageThumbnailTagHelper:TagHelper
    {
        public string ImageSrc { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName= "img";

            string fileName = ImageSrc.Split(".")[0];
            string fileExtensions=Path.GetExtension(ImageSrc);

            output.Attributes.SetAttribute("src", $"{fileName}{fileExtensions}");
        }
    }
}
