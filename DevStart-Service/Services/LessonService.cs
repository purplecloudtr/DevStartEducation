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
    public class LessonService : ILessonService
    {
        private readonly IRepository<Lesson> _lessonRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public LessonService(IRepository<Lesson> lessonRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _lessonRepository = lessonRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<LessonViewModel>> GetAllAsync()
        {
            var lessons = await _lessonRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<LessonViewModel>>(lessons);
        }

        public async Task<LessonViewModel> GetByIdAsync(Guid id)
        {
            var lesson = await _lessonRepository.GetByIdAsync(id);
            return _mapper.Map<LessonViewModel>(lesson);
        }

        public async Task AddAsync(LessonViewModel lessonViewModel)
        {
            var lesson = _mapper.Map<Lesson>(lessonViewModel);
            await _lessonRepository.AddAsync(lesson);
            await _unitOfWork.CommitAsync();
        }

        public async Task UpdateAsync(LessonViewModel lessonViewModel)
        {
            var lesson = _mapper.Map<Lesson>(lessonViewModel);
            _lessonRepository.Update(lesson);
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var lesson = await _lessonRepository.GetByIdAsync(id);
            if (lesson != null)
            {
                _lessonRepository.Delete(lesson);
                await _unitOfWork.CommitAsync();
            }
        }

        public async Task<IEnumerable<LessonViewModel>> GetLessonsByCourseIdAsync(Guid courseId)
        {
            var lessons = await _lessonRepository.GetAll(l => l.CourseId == courseId);
            return _mapper.Map<IEnumerable<LessonViewModel>>(lessons);
        }
    }
}
