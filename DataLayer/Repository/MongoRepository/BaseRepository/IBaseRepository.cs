using MongoDB.Bson;
using EntityLayer.PanteonEntity.MongoEntity.BaseEntity;
using System.Linq.Expressions;
using CrossCutting.CoreModels;

namespace DataLayer.Repository.MongoRepository.BaseRepository
{
    public interface IBaseRepository<T> where T : BaseEntityModel
    {
        Task<IEnumerable<T>> GetAllAsync();

        Task<List<T>> GetActiveAsync(Expression<Func<T, bool>>? filter);
        Task<T> GetByIdAsync(ObjectId id);
        Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> filter);
        Task<bool> AnyAsync(Expression<Func<T, bool>> filter);
        Task<OperationResult> AddAsync(T entity);
        Task UpdateAsync(ObjectId id, T entity);
        Task DeleteAsync(ObjectId id);
    }
}
