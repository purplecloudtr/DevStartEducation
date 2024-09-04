using DevStart_DataAccess.Repositories;
using DevStart_DataAccsess.Contexts;
using DevStart_Entity.Interfaces;
using DevStart_Entity.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevStart_DataAccsess.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork //interface'i implement ediyoruz.
    {
        private readonly DevStartDbContext _context;
        private bool disposed = false; // takip işlemi için kullanılacak bir değişken.

        public UnitOfWork(DevStartDbContext context)
        {
            _context = context;
        }

        public void Commit()
        {
            _context.SaveChanges(); // Veritabanına yapılan tüm değişiklikleri kaydediyoruz.
        }

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync(); // Asenkron olarak veritabanına yapılan tüm değişiklikleri kaydediyoruz.
        }

        public void Dispose()
        {
            _context.Dispose();//işlemlerden sonra kaynakların serbest bırakılması.
        }

        public IRepository<T> GetRepository<T>() where T : class
        {
            return new Repository<T>(_context); //// Generic Repository kullanarak yeni bir repository döndürüyoruz.
        }
    }
}
