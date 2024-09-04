using DevStart_Entity.Interfaces;
using DevStart_Entity.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DevStart_WebMvcUI.Controllers
{
    public class CourseHomeController : Controller
    {
        private readonly ICourseService _courseService;

        public CourseHomeController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        public async Task<IActionResult> Index()
        {
            var course = await _courseService.GetAllAsync();
            return View(course);
        }
    }
}
