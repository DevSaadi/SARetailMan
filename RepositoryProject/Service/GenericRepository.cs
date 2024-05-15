using Microsoft.EntityFrameworkCore;
using RepositoryProject.DBContext;
using RepositoryProject.Service.Interface;
using System.Linq.Expressions;

namespace RepositoryProject.Service
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly AppDbContext _dbcontext;
        public GenericRepository(AppDbContext context)
        {
            _dbcontext = context;
        }
        public async Task<T> Get(Expression<Func<T, bool>> filter)
        {
            try
            {
                T entity = await _dbcontext.Set<T>().FirstOrDefaultAsync(filter);
                return entity;
            }
            catch
            {
                throw;
            }
        }

        public async Task<T> Add(T entity)
        {
            try
            {

                _dbcontext.Set<T>().Add(entity);
                await _dbcontext.SaveChangesAsync();
                return entity;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> Edit(T entity)
        {
            try
            {
                _dbcontext.Update(entity);
                await _dbcontext.SaveChangesAsync();
                return true;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> Delete(T entity)
        {
            try
            {
                _dbcontext.Remove(entity);
                await _dbcontext.SaveChangesAsync();
                return true;
            }
            catch
            {
                throw;
            }
        }

        public async Task<IQueryable<T>> Query(Expression<Func<T, bool>> filter)
        {
            IQueryable<T> queryentity = filter == null ? _dbcontext.Set<T>() : _dbcontext.Set<T>().Where(filter);
            return queryentity;
        }
    }
}
