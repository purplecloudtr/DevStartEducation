using DevStart_Entity.Entities;
using DevStart_Entity.Interfaces;
using DevStart_Entity.ViewModels;
using DevStart_Service.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace DevStart_WebMvcUI.Controllers
{
    [Authorize]
    public class UserCourseController : Controller
    {
        private readonly ICourseService _courseService;
        private readonly ICourseSaleService _courseSaleService;
        private readonly ICourseSaleDetailService _courseSaleDetailService;

        public UserCourseController(ICourseService courseService, ICourseSaleService courseSaleService, ICourseSaleDetailService courseSaleDetailService)
        {
            _courseService = courseService;
            _courseSaleService = courseSaleService;
            _courseSaleDetailService = courseSaleDetailService;
        }

        public async Task<IActionResult> Index()
        {
            var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            Guid.TryParse(userIdString, out Guid userId);


            // Kullanıcının tüm CourseSale kayıtlarını alıyoruz
            var allCourseSales = await _courseSaleService.GetAllAsync();

            // Kullanıcıya ait olan CourseSale kayıtlarını filtreliyoruz
            var userSales = allCourseSales.Where(cs => cs.UserId == userId).ToList();

            // Kullanıcıya ait satış ID'lerini alıyoruz
            var saleIds = userSales.Select(s => s.CourseSaleId).ToList();

            // Tüm CourseSaleDetail kayıtlarını alıyoruz
            var allCourseDetails = await _courseSaleDetailService.GetAllAsync();

            // Satış ID'lerine göre CourseSaleDetail kayıtlarını filtreliyoruz
            var courseDetails = allCourseDetails.Where(cd => saleIds.Contains(cd.CourseSaleId)).ToList();

            // Kullanıcıya ait CourseId'leri alıyoruz
            var courseIds = courseDetails.Select(cd => cd.CourseId).Distinct().ToList();

            // Tüm Course kayıtlarını alıyoruz
            var allCourses = await _courseService.GetAllAsync();

            // Kullanıcıya ait olan Course kayıtlarını filtreliyoruz
            var courses = allCourses.Where(c => courseIds.Contains(c.CourseId)).ToList();

            // Sonuçları CourseViewModel olarak hazırlıyoruz
            var userCourses = courses.Select(course => new CourseViewModel
            {
                CourseId = course.CourseId,
                CourseTitle = course.CourseTitle,
                CourseDescription = course.CourseDescription,
                CoursePrice = course.CoursePrice,
                CourseCreateDate = course.CourseCreateDate,
                CourseState = course.CourseState,
                CategoryId = course.CategoryId,
                PictureUrl = course.PictureUrl,
                ShowCase = course.ShowCase,
                UserId = course.UserId
            }).ToList();

            return View(userCourses);
        }
    }
}
