using DevStart_DataAccsess.Identity;
using DevStart_Entity.Interfaces;
using DevStart_Entity.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
//using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;

namespace DevStart_WebMvcUI.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly UserManager<AppUser> _userManager;

        public AccountController(IAccountService accountService, UserManager<AppUser> userManager)
        {
            _accountService = accountService;
            _userManager = userManager;
        }

        // Index Action - Muhtemelen bir gösterge ya da ana sayfa
        public IActionResult Index()
        {
            return View();
        }
       
        public IActionResult Login(string? ReturnUrl)
        {
            LoginViewModel model = new LoginViewModel()
            {
                ReturnUrl = ReturnUrl //kullanıcıdan gelen returnurl'i alıyoruz bununla!
            };
            return View(model);
        }

        public IActionResult Register()
        {
            return View();
        }

        // Register Action - Sadece Register işlemi ile ilgilenecek
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            string msg = await _accountService.CreateUserAsync(model); //msg-> message benzeri.
            if (msg == "OK")
            {
                return RedirectToAction("Login");
            }
            else
            {
                ModelState.AddModelError("", msg);
            }
            return View(model);
        }

        // Login Action - Sadece Login işlemi ile ilgilenecek
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            string msg = await _accountService.FindByNameAsync(model);
            if (msg == "Kullanıcı bulunamadı!")
            {
                ModelState.AddModelError("", msg);
                return View(model);
            }
            else if (msg == "OK")
            {
                //if (User.IsInRole("Admin"))
                //{
                //    return Redirect(model.ReturnUrl ?? "/Dashboard");  //Admin Dashboard
                //}
                //else if (User.IsInRole("Yazar"))
                //{
                //    return Redirect(model.ReturnUrl ?? "/DashboardA"); // Yazar DashBoard
                //}

                return Redirect(model.ReturnUrl ?? "/Home");  // ?? null'sa sen anasayfaya git!
                
                
            }
            else
            {
                ModelState.AddModelError("", "Kullanıcı adı veya şifre hatalı!");

            }
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await _accountService.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
