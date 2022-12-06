using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MyAspNetCore.Web.Models;
using MyAspNetCore.Web.ViewModel;

namespace MyAspNetCore.Web.Views.Shared.ViewComponents
{
    public class VisitorViewComponent:ViewComponent
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public VisitorViewComponent(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var visitors=_context.Visitors.ToList();

            var visitorViewModels = _mapper.Map<List<VIsitorViewModel>>(visitors);

            ViewBag.visitorViewModels= visitorViewModels;

            return View();
        }

        
    }
}
