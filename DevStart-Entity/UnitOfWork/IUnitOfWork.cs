using DevStart_Entity.Entities;
using DevStart_Entity.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevStart_Entity.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<T> GetRepository<T>() where T : class;  //herhangi bir entity için kullanılabilir hale getiriyor iunitofwork.
        void Commit(); // savechanges çağırmak için
        Task CommitAsync(); // savechangesasync çağırmak için


        //IRepository<Category> Categories { get; } //böyle mi hocanın yaptığı üsttekiler gibi mi??
        //IRepository<Course> Courses { get; }
        //IRepository<CourseSale> CourseSales { get; }
        //IRepository<CourseSaleDetail> CourseSaleDetails { get; }
        //IRepository<Lesson> Lessons { get; }
        //IRepository<Review> Reviews { get; }
        //IRepository<Tag> Tags { get; }
        //IRepository<Video> Videos { get; }

        //Task<int> SaveChangesAsync();
    }
}
