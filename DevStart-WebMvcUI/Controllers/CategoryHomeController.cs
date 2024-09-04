using DevStart_Entity.Interfaces;
using DevStart_Service.Services;
using Microsoft.AspNetCore.Mvc;

namespace DevStart_WebMvcUI.Controllers
{
    public class CategoryHomeController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryHomeController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        public async Task<IActionResult> Index()
        {
            var categories = await _categoryService.GetAllAsync();
            return View(categories);
        }
    }
}
