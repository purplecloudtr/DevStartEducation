using DevStart_Entity.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevStart_Entity.Interfaces
{
    public interface ITagService
    {
        Task<IEnumerable<TagViewModel>> GetAllAsync();
        Task<TagViewModel> GetByIdAsync(Guid id);
        Task AddAsync(TagViewModel tagViewModel);
        Task UpdateAsync(TagViewModel tagViewModel);
        Task DeleteAsync(Guid id);

    }
}
