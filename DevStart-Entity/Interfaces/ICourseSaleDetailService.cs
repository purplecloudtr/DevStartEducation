using DevStart_Entity.Entities;
using DevStart_Entity.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevStart_Entity.Interfaces
{
    public interface ICourseSaleDetailService
    {
        Task<IEnumerable<CourseSaleDetailViewModel>> GetAllAsync();
        Task<CourseSaleDetailViewModel> GetByIdAsync(Guid id);
        Task AddAsync(CourseSaleDetailViewModel courseSaleDetailViewModel);
        Task UpdateAsync(CourseSaleDetailViewModel courseSaleDetailViewModel);
        Task DeleteAsync(Guid id);
        Task<IEnumerable<CourseSaleDetailViewModel>> GetDetailsByCourseSaleIdAsync(Guid courseSaleId);
    }
}
