using AutoMapper;
using DevStart_DataAccsess.Identity;
using DevStart_Entity.Interfaces;
using DevStart_Entity.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DevStart_Service.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IMapper _mapper;

        public AccountService(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, SignInManager<AppUser> signInManager, IMapper mapper)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _mapper = mapper;
        }

        public Task<string> CreateRoleAsync(RoleViewModel model)
        {
            throw new NotImplementedException();
        }

        public async Task<string> CreateUserAsync(RegisterViewModel model)
        {
            string message = string.Empty;
            AppUser user = new AppUser()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                UserName = model.UserName
            };
            var identityResult = await _userManager.CreateAsync(user, model.Password); // createasync methodu için awaitle işaretledik. üstteki IActionResult'tı da Task<> içine aldık. öncesinde de async ile işaretledik. ve identityResult ismindeki değişkene atadık!
            if (identityResult.Succeeded)
            {
                message = "OK";
            }
            else
            {
                foreach (var error in identityResult.Errors)
                {
                    message = error.Description;
                }
            }
            return message;
        }

        public Task DeleteRoleAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<UserViewModel> Find(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            return _mapper.Map<UserViewModel>(user);
        }

        public Task<List<RoleViewModel>> GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public async Task<List<UserViewModel>> GetAllUsers()
        {
            var list = await _userManager.Users.ToListAsync();
            return _mapper.Map<IEnumerable<UserViewModel>>(list).ToList();
        }

        public async Task<string> FindByNameAsync(LoginViewModel model)
        {
            string message = string.Empty;
            var user = await _userManager.FindByNameAsync(model.UserName); //ekrandan gelen user modeli ona veriyoruz. await ekledik aysnc olduğundan. sonra üst tarafa async ve Task<> ilave ettik.
            if (user == null)
            {
                message = "Kullanıcı bulunamadı!";
                return message; //buraları service katmanı olduğundan view kullanmıyoruz BURALARDA!!
            }
            var signInResult = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false); //sonraki false lockout özelliğini inaktif yapıyor.

            if (signInResult.Succeeded)
            {
                message = "OK";
            }
            return message;
        }

        public async Task SignOutAsync()
        {
            await _signInManager.SignOutAsync();
        }

       
    }
}
