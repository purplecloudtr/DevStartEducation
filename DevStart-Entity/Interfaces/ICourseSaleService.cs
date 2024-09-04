using DevStart_Entity.Entities;
using DevStart_Entity.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevStart_Entity.Interfaces
{
    public interface ICourseSaleService
    {
        Task<IEnumerable<CourseSaleViewModel>> GetAllAsync();
        Task<CourseSaleViewModel> GetByIdAsync(Guid id);
        Task AddAsync(CourseSaleViewModel courseSaleViewModel);
        Task UpdateAsync(CourseSaleViewModel courseSaleViewModel);
        Task DeleteAsync(Guid id);
    }
}
