using FluentValidation;
using MyAspNetCore.Web.ViewModel;

namespace MyAspNetCore.Web.Validators
{
    public class ProductValidator: AbstractValidator<ProductViewModel>
    {
        public ProductValidator()
        {
            RuleFor(x => x.Name).NotNull().WithMessage("{PropertyName} is required").NotEmpty().WithMessage("{PropertyName} is required");
            RuleFor(x => x.Price).NotNull().InclusiveBetween(1, int.MaxValue).WithMessage("{PropertyName} must be greater 0 ");
            RuleFor(x => x.Stock).NotNull().InclusiveBetween(1, int.MaxValue).WithMessage("{PropertyName} must be greater 0 ");
            

        }
    }
}
