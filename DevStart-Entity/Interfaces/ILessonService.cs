using DevStart_Entity.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevStart_Entity.Interfaces
{
    public interface ILessonService
    {
        Task<IEnumerable<LessonViewModel>> GetAllAsync();
        Task<LessonViewModel> GetByIdAsync(Guid id);
        Task AddAsync(LessonViewModel lessonViewModel);
        Task UpdateAsync(LessonViewModel lessonViewModel);
        Task DeleteAsync(Guid id);
        Task<IEnumerable<LessonViewModel>> GetLessonsByCourseIdAsync(Guid courseId);

    }
}
