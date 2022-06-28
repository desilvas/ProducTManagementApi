using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ProductManagement.Data;

namespace ProductManagement.EntityFramework
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly DatabaseEntities _context;
        public BaseRepository(DatabaseEntities context)
        {
            _context = context;
        }

        public T Add(T entity)
        {
            return _context.Set<T>().Add(entity);
        }

        public void AddRange(IEnumerable<T> entities)
        {
            _context.Set<T>().AddRange(entities);
        }

        public async Task<IEnumerable<T>> Find(Expression<Func<T, bool>> expression)
        {
            return await _context.Set<T>().Where(expression).ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public T Find(Guid id)
        {
            return _context.Set<T>().Find(id);
        }

        public void Remove(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            _context.Set<T>().RemoveRange(entities);
        }

        public bool Exists(Guid id)
        {
            return Find(id) != null;
        }

        public T Update(T entity, T update)
        {
            if (update == null)
            {
                return null;
            }
            else
            {
                _context.Entry(entity).CurrentValues.SetValues(update);
                _context.Entry(entity).State = EntityState.Modified;
               
                return update;
            }
        }
    }
}
