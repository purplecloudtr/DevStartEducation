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
    public class ReviewService : IReviewService
    {
        private readonly IRepository<Review> _reviewRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public async Task AddAsync(ReviewViewModel reviewViewModel)
        {
            var review = _mapper.Map<Review>(reviewViewModel);
            await _reviewRepository.AddAsync(review);
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var review = await _reviewRepository.GetByIdAsync(id);
            if (review != null)
            {
                _reviewRepository.Delete(review);
                await _unitOfWork.CommitAsync();
            }
        }

        public async Task<IEnumerable<ReviewViewModel>> GetAllAsync()
        {
            var reviews = await _reviewRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ReviewViewModel>>(reviews);
        }

        public async Task<ReviewViewModel> GetByIdAsync(Guid id)
        {
            var review = await _reviewRepository.GetByIdAsync(id);
            return _mapper.Map<ReviewViewModel>(review);
        }

        public async Task<IEnumerable<ReviewViewModel>> GetReviewsByCourseIdAsync(Guid courseId)
        {
            var reviews = await _reviewRepository.GetAll(r => r.CourseId == courseId);
            return _mapper.Map<IEnumerable<ReviewViewModel>>(reviews);
        }

        public async Task UpdateAsync(ReviewViewModel reviewViewModel)
        {
            var review = _mapper.Map<Review>(reviewViewModel);
            _reviewRepository.Update(review);
            await _unitOfWork.CommitAsync();
        }
    }
}
