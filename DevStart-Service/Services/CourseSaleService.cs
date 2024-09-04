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
    public class CourseSaleService : ICourseSaleService
    {
        private readonly IRepository<CourseSale> _courseSaleRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CourseSaleService(IRepository<CourseSale> courseSaleRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _courseSaleRepository = courseSaleRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CourseSaleViewModel>> GetAllAsync()
        {
            var courseSales = await _courseSaleRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<CourseSaleViewModel>>(courseSales);
        }

        public async Task<CourseSaleViewModel> GetByIdAsync(Guid id)
        {
            var courseSale = await _courseSaleRepository.GetByIdAsync(id);
            return _mapper.Map<CourseSaleViewModel>(courseSale);
        }

        public async Task AddAsync(CourseSaleViewModel courseSaleViewModel) // Güncellenmiş
        {
            var courseSale = _mapper.Map<CourseSale>(courseSaleViewModel);
            await _courseSaleRepository.AddAsync(courseSale);
            await _unitOfWork.CommitAsync();
        }

        public async Task UpdateAsync(CourseSaleViewModel courseSaleViewModel)
        {
            var courseSale = _mapper.Map<CourseSale>(courseSaleViewModel);
            _courseSaleRepository.Update(courseSale);
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var courseSale = await _courseSaleRepository.GetByIdAsync(id);
            if (courseSale != null)
            {
                _courseSaleRepository.Delete(courseSale);
                await _unitOfWork.CommitAsync();
            }
        }
    }
}
