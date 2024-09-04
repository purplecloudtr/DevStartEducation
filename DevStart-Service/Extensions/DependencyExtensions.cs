using DevStart_DataAccess.Repositories;
using DevStart_DataAccsess.Contexts;
using DevStart_DataAccsess.Identity;
using DevStart_DataAccsess.UnitOfWorks;
using DevStart_Entity.Entities;
using DevStart_Entity.Interfaces;
using DevStart_Entity.UnitOfWork;
using DevStart_Service.Mapping;
using DevStart_Service.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevStart_Service.Extensions
{
    public static class DependencyExtensions
    {
        public static void AddExtensions(this IServiceCollection services)
        {
            services.AddIdentity<AppUser, AppRole>(
                opt =>
                {
                    opt.Password.RequiredLength = 1;    //default 6 karakter
                    opt.Password.RequireNonAlphanumeric = false;
                    opt.Password.RequireUppercase = false;
                    opt.Password.RequireLowercase = false;
                    opt.Password.RequireDigit = false;

                    opt.User.RequireUniqueEmail = false;  //aynı email adresinin tekrar kullanılmasına izin vermez.
                    /*opt.User.AllowedUserNameCharacters = "abcdefghijklmnoprstuvyz0123456789";*/ //kullanıcı adı girilirken bunlardan başka birkarakter girilmesine izin vermez.
                    opt.Lockout.MaxFailedAccessAttempts = 3;  //default 5
                    opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(1); //default 5
                }).AddEntityFrameworkStores<DevStartDbContext>();

			services.ConfigureApplicationCookie(opt =>
			{
				opt.LoginPath = new PathString("/Account/Login");
				opt.LogoutPath = new PathString("/Account/Logout");
				opt.ExpireTimeSpan = TimeSpan.FromMinutes(600);
				opt.SlidingExpiration = true; // 600 dakika olmadan yeniden login olursa süre yeniden başlar

				opt.Cookie = new CookieBuilder()
				{
					Name = "Wissen.Cookie",
					HttpOnly = true,
				};
			});

			services.AddAutoMapper(typeof(MappingProfile));

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ICourseService, CourseService>();
            services.AddScoped<ILessonService, LessonService>();
            services.AddScoped<ICourseSaleService, CourseSaleService>();
            services.AddScoped<ICourseSaleDetailService, CourseSaleDetailService>();

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>)); //generic repo kullanabilmemiz için.

            


        }

    }

}
