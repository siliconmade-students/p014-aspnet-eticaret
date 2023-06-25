using Eticaret.Business.Services;
using Eticaret.Web.Mvc.Models;
using Microsoft.AspNetCore.Mvc;

namespace Eticaret.Web.Mvc.Views.Shared.Components.Navbar;

public class NavbarViewComponent : ViewComponent
{
    private readonly ICategoryService _categoryService;

    public NavbarViewComponent(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var vm = new NavbarViewModel
        {
            Categories = _categoryService.GetAll(),
        };

        return View(vm);
    }
}