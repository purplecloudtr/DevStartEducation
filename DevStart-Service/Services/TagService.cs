using AutoMapper;
using DevStart_Entity.Entities;
using DevStart_Entity.Interfaces;
using DevStart_Entity.UnitOfWork;
using DevStart_Entity.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevStart_Service.Services
{
    public class TagService : ITagService
    {
        private readonly IRepository<Tag> _tagRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TagService(IRepository<Tag> tagRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _tagRepository = tagRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TagViewModel>> GetAllAsync()
        {
            var tags = await _tagRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<TagViewModel>>(tags);
        }

        public async Task<TagViewModel> GetByIdAsync(Guid id)
        {
            var tag = await _tagRepository.GetByIdAsync(id);
            return _mapper.Map<TagViewModel>(tag);
        }

        public async Task AddAsync(TagViewModel tagViewModel)
        {
            var tag = _mapper.Map<Tag>(tagViewModel);
            await _tagRepository.AddAsync(tag);
            await _unitOfWork.CommitAsync();
        }

        public async Task UpdateAsync(TagViewModel tagViewModel)
        {
            var tag = _mapper.Map<Tag>(tagViewModel);
            _tagRepository.Update(tag);
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var tag = await _tagRepository.GetByIdAsync(id);
            if (tag != null)
            {
                _tagRepository.Delete(tag);
                await _unitOfWork.CommitAsync();
            }
        }
    }
}
