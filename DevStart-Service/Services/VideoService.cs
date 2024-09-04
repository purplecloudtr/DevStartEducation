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
    public class VideoService
    {
        private readonly IRepository<Video> _videoRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public VideoService(IRepository<Video> videoRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _videoRepository = videoRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<VideoViewModel>> GetAllAsync()
        {
            var videos = await _videoRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<VideoViewModel>>(videos);
        }

        public async Task<VideoViewModel> GetByIdAsync(Guid id)
        {
            var video = await _videoRepository.GetByIdAsync(id);
            return _mapper.Map<VideoViewModel>(video);
        }

        public async Task AddAsync(VideoViewModel videoViewModel)
        {
            var video = _mapper.Map<Video>(videoViewModel);
            await _videoRepository.AddAsync(video);
            await _unitOfWork.CommitAsync();
        }

        public async Task UpdateAsync(VideoViewModel videoViewModel)
        {
            var video = _mapper.Map<Video>(videoViewModel);
            _videoRepository.Update(video);
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var video = await _videoRepository.GetByIdAsync(id);
            if (video != null)
            {
                _videoRepository.Delete(video);
                await _unitOfWork.CommitAsync();
            }
        }
    }
}
