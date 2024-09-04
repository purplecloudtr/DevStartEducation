using DevStart_Entity.Interfaces;
using DevStart_Service.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DevStart_WebMvcUI.Controllers
{
    public class UserSalesController : Controller
    {
        private readonly ICourseService _courseService;
        private readonly ICourseSaleService _courseSaleService;
        private readonly ICourseSaleDetailService _courseSaleDetailService;

        public UserSalesController(ICourseService courseService, ICourseSaleService courseSaleService, ICourseSaleDetailService courseSaleDetailService)
        {
            _courseService = courseService;
            _courseSaleService = courseSaleService;
            _courseSaleDetailService = courseSaleDetailService;
        }

        public async Task<IActionResult> Index()
        {
            var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            Guid.TryParse(userIdString, out Guid userId);

            var sales = await _courseSaleService.GetAllAsync();
            var userSales = sales.Where(s=> s.UserId == userId).ToList();

            return View(userSales);
        }

        [HttpGet]
        public async Task<JsonResult> GetDetail(Guid courseSaleId)
        {
            var details = await _courseSaleDetailService.GetDetailsByCourseSaleIdAsync(courseSaleId);

            var jsonResponse = new List<object>();

            foreach (var detail in details)
            {
                // CourseId ile ilgili kursu alıyoruz
                var course = await _courseService.GetByIdAsync(detail.CourseId);

                jsonResponse.Add(new
                {
                    count = detail.CourseSaleDetailQuantity,
                    courseId = detail.CourseId,
                    courseTitle = course?.CourseTitle,
                    courseDescription = course?.CourseDescription,
                    coursePrice = course?.CoursePrice,
                });
            }

            return Json(jsonResponse);

            //// courseId'yi kullanarak veritabanından dersleri alıyoruz
            //var list = await _courseSaleDetailService.GetDetailsByCourseSaleIdAsync(courseSaleId);

            //// Dersleri JSON verisi olarak döndürmek için bir liste oluşturuyoruz
            //var jsonResponse = list.Select(detail => new
            //{
            //    count = detail.CourseSaleDetailQuantity,
            //    courseId = detail.CourseId,
            //    courseTitle = detail.Course?.CourseTitle
            //}).ToList();

            //return Json(jsonResponse);
        }
    }
}
