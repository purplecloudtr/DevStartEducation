using DevStart_Entity.Interfaces;
using DevStart_Entity.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace DevStart_WebMvcUI.Controllers
{
    [Authorize(Roles = "Yazar")]
    public class LessonController : Controller
    {
        private readonly ICourseService _courseService;
        private readonly ICategoryService _categoryService;
        private readonly ILessonService _lessonService;

        public LessonController(ICourseService courseService, ICategoryService categoryService, ILessonService lessonService)
        {
            _courseService = courseService;
            _categoryService = categoryService;
            _lessonService = lessonService;
        }


        public async Task<IActionResult> Index()
        {
            var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            Guid.TryParse(userIdString, out Guid userId);
            

            var courses = await _courseService.GetAllAsync();
            var userCourses = courses.Where(c => c.UserId == userId).ToList();

            ViewBag.Courses = new SelectList(userCourses, "CourseId", "CourseTitle");
            return View(new LessonViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Index(LessonViewModel model, IFormFile VideoLink)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot//images", VideoLink.FileName);
            using (var stream = new FileStream(path, FileMode.Create))
            {
                await VideoLink.CopyToAsync(stream);
            }

            model.VideoLink = "/images/" + VideoLink.FileName;


            await _lessonService.AddAsync(model);

            TempData["message1"] = true;
            TempData["message2"] = "Ders başarıyla kayıt edildi.";

            var courses = await _courseService.GetAllAsync();
            ViewBag.Courses = new SelectList(courses, "CourseId", "CourseTitle");

            return View(model);
        }

    }
}
