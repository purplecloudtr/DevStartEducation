using DevStart_DataAccsess.Identity;
using DevStart_Entity.Entities;
using DevStart_Entity.Interfaces;
using DevStart_Entity.ViewModels;
using DevStart_Service.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace DevStart_WebMvcUI.Controllers
{
    [Authorize(Roles = "Yazar")]
    public class CourseController : Controller
    {
        private readonly ICourseService _courseService;
        private readonly ICategoryService _categoryService;
        private readonly UserManager<AppUser> _userManager;

        public CourseController(UserManager<AppUser> userManager, ICourseService courseService, ICategoryService categoryService) //DI Container -> CourseService
        {
            _courseService = courseService;
            _categoryService = categoryService;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var courses = await _courseService.GetAllAsync();
            var categories = await _categoryService.GetAllAsync();

            var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;            
            if (Guid.TryParse(userIdString, out Guid userId))
            {
                courses = courses.Where(c => c.UserId == userId);
            }

            ViewBag.Categories = new SelectList(categories, "CategoryId", "CategoryName");
            return View((new CourseViewModel(), courses));
        }

        [HttpPost]
        public async Task<IActionResult> Index(CourseViewModel model, IFormFile PictureUrl)
        {
            //if (ModelState.IsValid)
            // {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot//images", PictureUrl.FileName);
            using (var stream = new FileStream(path, FileMode.Create))
            {
                await PictureUrl.CopyToAsync(stream);
            }

            model.PictureUrl = "/images/" + PictureUrl.FileName;

            var user = await _courseService.Find(User.Identity.Name);
            model.UserId = user.Id;

            await _courseService.AddAsync(model);

            TempData["message1"] = true;
            TempData["message2"] = "Kurs başarıyla kayıt edildi.";

            var courses = await _courseService.GetAllAsync();
            var categories = await _categoryService.GetAllAsync();


            var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (Guid.TryParse(userIdString, out Guid userId))
            {
                courses = courses.Where(c => c.UserId == userId);
            }

            ViewBag.Categories = new SelectList(categories, "CategoryId", "CategoryName");

            return View((model, courses));

        }


        [HttpPost]
        public async Task<IActionResult> Create(CourseViewModel model, IFormFile formFile)
        {
            if (ModelState.IsValid)
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot//images", formFile.FileName);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await formFile.CopyToAsync(stream);
                }

                model.PictureUrl = "/images/" + formFile.FileName;

                var user = await _courseService.Find(User.Identity.Name);
                model.UserId = user.Id;

                await _courseService.AddAsync(model);

                TempData["message1"] = true;
                TempData["message2"] = "Kurs başarıyla kayıt edildi.";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["message1"] = false;
                TempData["message2"] = "Kurs kayıt edilemedi.";
                return RedirectToAction("Index");
            }
        }
        public async Task<IActionResult> Update(CourseViewModel courseViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(courseViewModel);
            }

            try
            {
                await _courseService.UpdateAsync(courseViewModel); // Asenkron güncellemeyi gerçekleştir
                var updatedCourse = await _courseService.GetByIdAsync(courseViewModel.CourseId); // Güncellenmiş veriyi al
                var viewModel = new CourseViewModel
                {
                    CourseId = updatedCourse.CourseId,
                    CourseDescription = updatedCourse.CourseDescription,
                    CoursePrice = updatedCourse.CoursePrice,
                    CourseCreateDate = updatedCourse.CourseCreateDate,
                    PictureUrl = updatedCourse.PictureUrl,
                    CategoryId = updatedCourse.CategoryId

                };

                return View(viewModel); // Güncellenmiş veriyi göster
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Güncelleme sırasında bir hata oluştu: " + ex.Message);
                return View(courseViewModel);
            }

        }
        public async Task<IActionResult> Delete(Guid CourseId)
        {
            await _courseService.DeleteAsync(CourseId);
            return RedirectToAction("Index");
        }
    }
}
