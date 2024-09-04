using Azure;
using DevStart_DataAccsess.Identity;
using DevStart_Entity.Interfaces;
using DevStart_Entity.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DevStart_WebMvcUI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        //******ADMIN TARAFI CONTROLLER******//

        private readonly IAccountService _accountService;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly SignInManager<AppUser> _signInManager;

        public UserController(IAccountService accountService, UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, SignInManager<AppUser> signInManager, ICategoryService categoryService)
        {
            _accountService = accountService;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.ToListAsync();
            // Kullanıcıların rollerini almak için UserManager'ı kullanıyoruz
            var userViewModels = new List<UserViewModel>();

            foreach (var user in users)
            {
                var _roles = await _userManager.GetRolesAsync(user);
                var roleName = _roles.FirstOrDefault(); // Kullanıcının ilk ve tek rolünü alıyoruz

                userViewModels.Add(new UserViewModel
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    PhoneNumber = user.PhoneNumber,
                    RoleName = roleName // Tek rol adını ekliyoruz
                });
            }

            var roles = _roleManager.Roles.Select(r => new RoleViewModel
            {
                Id = r.Id,
                Name = r.Name,
                Description = ""
            }).ToList();

            return View((userViewModels, roles));
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            string message = await _accountService.CreateUserAsync(model);

            if (message == "Kullanıcı başarıyla oluşturuldu.")
            {
                TempData["message1"] = true;
                TempData["message2"] = "Kayıt Başarılı";
            }
            else
            {
                TempData["message1"] = false;
                TempData["message2"] = message;
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> AssignRoleToUser(Guid userId, Guid roleId)
        {
            // Kullanıcı ve rol bilgilerini alıyoruz
            var user = await _userManager.FindByIdAsync(userId.ToString());
            var role = await _roleManager.FindByIdAsync(roleId.ToString());

            if (user == null || role == null)
            {
                TempData["message1"] = "err";
                TempData["message2"] = "Kullanıcı veya yetki bulunamadı!";
                return RedirectToAction("Index");
            }

            // Kullanıcının mevcut rollerini alıyoruz
            var userRoles = await _userManager.GetRolesAsync(user);

            // Mevcut tüm rolleri kaldırıyoruz
            var removeRolesResult = await _userManager.RemoveFromRolesAsync(user, userRoles);
            if (!removeRolesResult.Succeeded)
            {
                TempData["message1"] = "err";
                TempData["message2"] = "Mevcut roller kaldırılırken bir hata oluştu!";
                return RedirectToAction("Index");
            }

            // Yeni rolü kullanıcıya atıyoruz
            var addResult = await _userManager.AddToRoleAsync(user, role.Name);

            if (addResult.Succeeded)
            {
                TempData["message1"] = "ok";
                TempData["message2"] = "Yetki ataması başarıyla güncellendi.";
                return RedirectToAction("Index");
            }

            // Hata oluşması durumunda
            TempData["message1"] = "err";
            TempData["message2"] = "Yetki ataması yapılırken bir hata oluştu!";
            return RedirectToAction("Index");
        }


        public async Task<IActionResult> Logout()
        {
            await _accountService.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        //[HttpPost]
        //public async Task<IActionResult> Register(RegisterViewModel model)
        //{
        //    Response response = new Response();
        //    response = await _accountService.CreateUserAsync(model);

        //    if (response.Success)
        //    {
        //        TempData["message1"] = response.Success;
        //        TempData["message2"] = "Kayıt Başarılı";
        //    }
        //    else
        //    {
        //        TempData["message1"] = response.Success;
        //        TempData["message2"] = response.Message;
        //    }
        //    return RedirectToAction("Index");
        //}
    }
}
