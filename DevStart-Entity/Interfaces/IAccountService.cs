using DevStart_Entity.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DevStart_Entity.Interfaces
{
    public interface IAccountService
    {
        Task<string> CreateUserAsync(RegisterViewModel model);
        Task<UserViewModel> Find(string username);
        Task<string> FindByNameAsync(LoginViewModel model);
        Task<string> CreateRoleAsync(RoleViewModel model);

        Task<List<UserViewModel>> GetAllUsers();
        Task<List<RoleViewModel>> GetAllRoles();

        //Task<string> EditRoleListAsync(EditRoleViewModel model);
        Task DeleteRoleAsync(string id);
        Task SignOutAsync();
    }
}
