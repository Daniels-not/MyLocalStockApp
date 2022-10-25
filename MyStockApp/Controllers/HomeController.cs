using Microsoft.AspNetCore.Mvc;
using MyStockApp.Core.Application.Interfaces.Services;
using MyStockApp.Core.Application.ViewModels.Products;
using MyStockApp.Middlewares;

namespace MyStockApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly ValidateUserSession _validateUserSession;


        public HomeController(IProductService productService, ICategoryService categoryService, ValidateUserSession validateUserSession)
        {
            _productService = productService;
            _categoryService = categoryService;
            _validateUserSession = validateUserSession;
        }

        public async Task<IActionResult> Index(FilterProductViewModel data)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }

            ViewBag.Categories = await _categoryService.GetAllViewModel();
            return View(await _productService.GetAllViewModelWithFilters(data));
        }

        public async Task<IActionResult> ProductDetails(int id)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }

            ProductViewModel? product = await _productService.GetProductDetails(id);

            return View(product);
        }
    }
}
