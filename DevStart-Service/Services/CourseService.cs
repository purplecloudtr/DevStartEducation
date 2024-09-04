using AutoMapper;
using DevStart_DataAccsess.Identity;
using DevStart_Entity.Entities;
using DevStart_Entity.Interfaces;
using DevStart_Entity.UnitOfWork;
using DevStart_Entity.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevStart_Service.Services
{
    public class CourseService : ICourseService
    {
        private readonly IRepository<Course> _courseRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;

        public CourseService(IRepository<Course> courseRepository, IUnitOfWork unitOfWork, IMapper mapper, UserManager<AppUser> userManager)
        {
            _courseRepository = courseRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<IEnumerable<CourseViewModel>> GetAllAsync()
        {
            var courses = await _courseRepository.GetAllAsync();
            return _mapper.Map<List<CourseViewModel>>(courses);
        }
        public async Task<UserViewModel> Find(string userName) //BAKILACAAAAAAAAKK!
        {
            var user = await _userManager.FindByNameAsync(userName);
            return _mapper.Map<UserViewModel>(user);
        }
        public async Task<CourseViewModel> GetByIdAsync(Guid id)
        {
            var course = await _courseRepository.GetByIdAsync(id);
            return _mapper.Map<CourseViewModel>(course);
        }

        public async Task AddAsync(CourseViewModel courseViewModel)
        {
            var course = _mapper.Map<Course>(courseViewModel);
            await _courseRepository.AddAsync(course);
            await _unitOfWork.CommitAsync();
        }

        public async Task UpdateAsync(CourseViewModel courseViewModel)
        {
            var course = _mapper.Map<Course>(courseViewModel);
            _courseRepository.Update(course);
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteAsync(Guid CourseId)
        {
            var course = await _courseRepository.GetByIdAsync(CourseId);
            if (course != null)
            {
                _courseRepository.Delete(course);
                await _unitOfWork.CommitAsync();
            }
        }


        public async Task<IEnumerable<CourseViewModel>> GetCoursesByCategoryIdAsync(Guid categoryId)
        {
            var courses = await _courseRepository.GetAll(c => c.CategoryId == categoryId);
            return _mapper.Map<IEnumerable<CourseViewModel>>(courses);
        }

        public async Task AddTagToCourseAsync(Guid courseId, Guid tagId)
        {
            var course = await _courseRepository.GetByIdAsync(courseId);
            if (course != null)
            {
                // Eğer tag eklenmemişse, ilişkiye ekle
                if (!course.Tags.Any(t => t.TagId == tagId))
                {
                    var tag = new Tag { TagId = tagId }; // Sadece TagId ile örnek yaratıyoruz
                    course.Tags.Add(tag);
                    await _unitOfWork.CommitAsync();
                }
            }
        }
        public async Task RemoveTagFromCourseAsync(Guid courseId, Guid tagId)
        {
            var course = await _courseRepository.GetByIdAsync(courseId);
            if (course != null)
            {
                var tagToRemove = course.Tags.FirstOrDefault(t => t.TagId == tagId);
                if (tagToRemove != null)
                {
                    course.Tags.Remove(tagToRemove);
                    await _unitOfWork.CommitAsync();
                }
            }
        }
    }
}
