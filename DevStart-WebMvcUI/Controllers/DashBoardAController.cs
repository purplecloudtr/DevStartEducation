using Microsoft.AspNetCore.Mvc;

namespace DevStart_WebMvcUI.Controllers
{
    public class DashBoardAController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
