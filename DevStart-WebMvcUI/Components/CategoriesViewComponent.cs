using DevStart_Entity.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DevStart_WebMvcUI.Components
{
    public class CategoriesViewComponent : ViewComponent
    {
        private readonly ICategoryService _categoryService;

        public CategoriesViewComponent(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var category = await _categoryService.GetAllAsync();
            return View("Index", category);
        }

    }
}
