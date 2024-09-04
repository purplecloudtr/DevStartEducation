using DevStart_Entity.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevStart_Entity.Interfaces
{
    public interface ICourseService
    {
        Task<IEnumerable<CourseViewModel>> GetAllAsync();
        Task<CourseViewModel> GetByIdAsync(Guid id);
        Task AddAsync(CourseViewModel courseViewModel);
        Task UpdateAsync(CourseViewModel courseViewModel);
        Task DeleteAsync(Guid id);
        Task<IEnumerable<CourseViewModel>> GetCoursesByCategoryIdAsync(Guid categoryId);

        Task<UserViewModel> Find(string username);

    }
}
