using System.Linq.Expressions;

namespace RepositoryProject.Service.Interface
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> Get(Expression<Func<T, bool>> filter);
        Task<T> Add(T entity);
        Task<bool> Edit(T entity);
        Task<bool> Delete(T entity);
        Task<IQueryable<T>> Query(Expression<Func<T, bool>> filter = null);
    }
}
