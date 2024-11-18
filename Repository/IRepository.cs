using System.Linq.Expressions;

namespace Repository
{
    public interface IRepository<T> where T : class
    {
        Task AddAsync(T entity);
        Task DeleteAsync(T entity);
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includeProperties);
        Task<T> GetByIdAsync(int? id);
        Task<T> GetByIdAsync(int? id, params Expression<Func<T, object>>[] includeProperties);
        Task UpdateAsync(T entity);
    }
}