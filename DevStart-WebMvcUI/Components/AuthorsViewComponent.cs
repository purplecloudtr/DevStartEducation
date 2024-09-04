using DevStart_DataAccsess.Identity;
using DevStart_Entity.Interfaces;
using DevStart_Entity.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DevStart_WebMvcUI.Components
{
    public class AuthorsViewComponent : ViewComponent
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;

        public AuthorsViewComponent(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var allUsers = await _userManager.Users.ToListAsync();
            var authorUsers = new List<UserViewModel>();

            foreach (var user in allUsers)
            {
                var roles = await _userManager.GetRolesAsync(user);
                var roleName = roles.FirstOrDefault(); // Kullanıcının ilk rolünü alıyoruz

                if (roleName == "Yazar") // "Yazar" rolüne sahip kullanıcıları filtreliyoruz
                {
                    authorUsers.Add(new UserViewModel
                    {
                        Id = user.Id,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        UserName = user.UserName,
                        PhoneNumber = user.PhoneNumber,
                        Email = user.Email,
                        RoleName = roleName // Kullanıcının rol adını ekliyoruz
                    });
                }
            }

            return View("Index", authorUsers);
        }
    }
}
