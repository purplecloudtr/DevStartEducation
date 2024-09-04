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
    public class CourseSaleDetailService : ICourseSaleDetailService
    {
        private readonly IRepository<CourseSaleDetail> _courseSaleDetailRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CourseSaleDetailService(IRepository<CourseSaleDetail> courseSaleDetailRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _courseSaleDetailRepository = courseSaleDetailRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CourseSaleDetailViewModel>> GetAllAsync()
        {
            var courseSaleDetails = await _courseSaleDetailRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<CourseSaleDetailViewModel>>(courseSaleDetails);
        }

        public async Task<CourseSaleDetailViewModel> GetByIdAsync(Guid id)
        {
            var courseSaleDetail = await _courseSaleDetailRepository.GetByIdAsync(id);
            return _mapper.Map<CourseSaleDetailViewModel>(courseSaleDetail);
        }

        public async Task AddAsync(CourseSaleDetailViewModel courseSaleDetailViewModel)
        {
            var courseSaleDetail = _mapper.Map<CourseSaleDetail>(courseSaleDetailViewModel);
            await _courseSaleDetailRepository.AddAsync(courseSaleDetail);
            await _unitOfWork.CommitAsync();
        }

        public async Task UpdateAsync(CourseSaleDetailViewModel courseSaleDetailViewModel)
        {
            var courseSaleDetail = _mapper.Map<CourseSaleDetail>(courseSaleDetailViewModel);
            _courseSaleDetailRepository.Update(courseSaleDetail);
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var courseSaleDetail = await _courseSaleDetailRepository.GetByIdAsync(id);
            if (courseSaleDetail != null)
            {
                _courseSaleDetailRepository.Delete(courseSaleDetail);
                await _unitOfWork.CommitAsync();
            }
        }

        public async Task<IEnumerable<CourseSaleDetailViewModel>> GetDetailsByCourseSaleIdAsync(Guid courseSaleId)
        {
            var courseSaleDetails = await _courseSaleDetailRepository.GetAll(c => c.CourseSaleId == courseSaleId);
            return _mapper.Map<IEnumerable<CourseSaleDetailViewModel>>(courseSaleDetails);
        }
    }
}
