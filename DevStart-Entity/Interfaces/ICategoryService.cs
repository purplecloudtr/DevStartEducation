using DevStart_Entity.Entities;
using DevStart_Entity.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevStart_Entity.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryViewModel>> GetAllAsync();
        Task<CategoryViewModel> GetByIdAsync(Guid id);
        Task AddAsync(CategoryViewModel categoryViewModel);
        Task UpdateAsync(CategoryViewModel categoryViewModel);
        Task DeleteAsync(Guid id);

    }
}
