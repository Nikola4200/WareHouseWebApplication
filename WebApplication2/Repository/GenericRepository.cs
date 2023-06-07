using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WarehouseWeb.Context;

namespace WarehouseWeb.Model.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {

        private readonly AppDbContext _DbContext;

        public GenericRepository(AppDbContext context)
        {
            _DbContext = context;
        }

        public async Task<bool> Add(T entity)
        {
            await _DbContext.Set<T>().AddAsync(entity);
            return true;
        }

        public async Task<bool> Delete(T entity)
        {
            _DbContext.Set<T>().Remove(entity);
            return true;
        }

        public async Task<bool> DeleteAll(List<T> entities)
        {
            _DbContext.Set<T>().RemoveRange(entities);
            return true;
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _DbContext.Set<T>().ToListAsync();
        }

        public async Task<T> GetById(long id)
        {
            return await _DbContext.Set<T>().FindAsync(id);
        }

        public IQueryable<T> GetQueryable<T>() where T : class
        {
            DbSet<T> set = _DbContext.Set<T>();

            if (set == null)
            {
                throw new Exception("cannot-get-db-query");
            }

            return set as IQueryable<T>;
        }

        public async Task<bool> Update(T entity)
        {
            _DbContext.Set<T>().Update(entity);
            return true;
        }
    }
}
