using CrossCutting.CoreModels;
using System.Linq.Expressions;


namespace DataLayer.Repository.MsRepository.BaseRepository
{
    public interface IBaseRepository<T> where T : class
    {
        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);
        Task<T?> GetActiveFirstOrDefaultAsync(Expression<Func<T, bool>>? predicate = null);
        Task<OperationResult> AddAsync(T entity);
        Task<OperationResult> UpdateAsync(T entity);
    }
}
