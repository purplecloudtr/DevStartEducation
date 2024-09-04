using AutoMapper;
using DevStart_DataAccsess.Identity;
using DevStart_DataAccsess.UnitOfWorks;
using DevStart_Entity.Entities;
using DevStart_Entity.UnitOfWork;
using DevStart_Entity.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevStart_Service.Mapping
{
    internal class MappingProfile : Profile
    {
        public MappingProfile() //ctor tanımlıyoruz işlemler için.
        {
            CreateMap<Category, CategoryViewModel>().ReverseMap();
            CreateMap<AppUser, UserViewModel>().ReverseMap();
            CreateMap<Course, CourseViewModel>().ReverseMap();
            CreateMap<Lesson, LessonViewModel>().ReverseMap();
            CreateMap<CourseSale, CourseSaleViewModel>().ReverseMap();
            CreateMap<CourseSaleDetail, CourseSaleDetailViewModel>().ReverseMap();
            CreateMap<RegisterViewModel, AppUser>();

        }
    }
}
