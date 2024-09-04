using DevStart_Entity.Interfaces;
using DevStart_Service.Services;
using Microsoft.AspNetCore.Mvc;

namespace DevStart_WebMvcUI.Controllers
{
    public class CourseDetailController : Controller
    {
        private readonly ILessonService _lessonService;

        public CourseDetailController(ILessonService lessonService)
        {
            _lessonService = lessonService;
        }

        public async Task<IActionResult> Index(Guid id)
        {
            var lesson = await _lessonService.GetLessonsByCourseIdAsync(id);
            return View(lesson);           
        }
    }
}
