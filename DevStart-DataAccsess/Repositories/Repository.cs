using DevStart_DataAccsess.Contexts;
using DevStart_Entity.Entities;
using DevStart_Entity.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DevStart_DataAccess.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly DevStartDbContext _context;
        private readonly DbSet<T> _dbSet;

        public Repository(DevStartDbContext context)
        {
            _context = context; //burada vertabanına karşılık arakatmanı elde ediyoruz!
            _dbSet = _context.Set<T>(); //burada da hangi tabloyla çalışacaksak o tabloyu elde ediyoruz!
        }

        public async Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderby = null, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            if (orderby != null) //kullanıcı herhangi bi kolonda sıralama yapmak istemişse
            {
                query = orderby(query);
            }

            //foreach (var tablo in includes) //BU OLMALI MI? HOCANIN PROJEDE VAR.
            //{
            //    query = query.Include(tablo);// tabloları includes içine ekleyecek!
            //}

            return await query.ToListAsync(); // normal listeye çevir ve geri döndür bize demek.
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.AsNoTracking().ToListAsync(); //AsNoTracking() -> EfCore verileri burada bunu yazdığımızda verileri takip etmiyor ve bize daha hızlı sonuç döndürüyor. (modified, deleted gibi)
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }

        public Task<T> Get(Expression<Func<T, bool>> filter)
        {
            return _dbSet.FirstOrDefaultAsync(filter);
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            //await _context.SaveChangesAsync();
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
            //_context.SaveChanges();
        }

        public void Delete(Guid id)
        {
            var entity = _dbSet.Find(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                //_context.SaveChanges();
            }
        }

        public void Delete(T entity)
        {
            if (entity.GetType().GetProperty("IsDeleted") != null)
            {
                entity.GetType().GetProperty("IsDeleted").SetValue(entity, true); //tabloda IsDeleted kullanıyorsak!!
                _dbSet.Update(entity); //bu ve üst satır fiziksel silme yapmayacaksak
            }
            else
            {
                _dbSet.Remove(entity); //burada da fiziksel silme yapılacaksa!
            }
        }
    }
}