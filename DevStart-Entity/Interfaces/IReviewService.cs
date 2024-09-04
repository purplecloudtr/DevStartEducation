using DevStart_Entity.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevStart_Entity.Interfaces
{
    public interface IReviewService
    {
        Task<IEnumerable<ReviewViewModel>> GetAllAsync();
        Task<ReviewViewModel> GetByIdAsync(Guid id);
        Task AddAsync(ReviewViewModel reviewViewModel);
        Task UpdateAsync(ReviewViewModel reviewViewModel);
        Task DeleteAsync(Guid id);
        Task<IEnumerable<ReviewViewModel>> GetReviewsByCourseIdAsync(Guid courseId);

    }
}
