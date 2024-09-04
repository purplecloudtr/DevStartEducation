using DevStart_Entity.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DevStart_WebMvcUI.Controllers
{
    public class CategoryDetailController : Controller
    {
        private readonly ICourseService _courseService;

        public CategoryDetailController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        public async Task<IActionResult> Index(Guid id)
        {
            var course = await _courseService.GetCoursesByCategoryIdAsync(id);
            return View(course);
        }
    }
}
