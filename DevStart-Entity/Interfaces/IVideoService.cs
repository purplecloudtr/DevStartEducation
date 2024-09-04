using DevStart_Entity.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevStart_Entity.Interfaces
{
    public interface IVideoService
    {
        Task<IEnumerable<VideoViewModel>> GetAllAsync();
        Task<VideoViewModel> GetByIdAsync(Guid id);
        Task AddAsync(VideoViewModel videoViewModel);
        Task UpdateAsync(VideoViewModel videoViewModel);
        Task DeleteAsync(Guid id);
    }
}
