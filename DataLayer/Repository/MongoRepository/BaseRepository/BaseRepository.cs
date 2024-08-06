using CrossCutting.CoreModels;
using EntityLayer.PanteonEntity.MongoEntity.BaseEntity;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace DataLayer.Repository.MongoRepository.BaseRepository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntityModel
    {
        private readonly IMongoDatabase _context;
        
        public BaseRepository()
        {
            _context  = new ApplicationMongoDbContext().Database;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.GetCollection<T>(typeof(T).Name).Find(_ => true).ToListAsync();
        }

        public async Task<List<T>> GetActiveAsync(Expression<Func<T, bool>>? filter = null)
        {
            var activeFilter = Builders<T>.Filter.Eq(e => e.ObjectStatus, 1);
            var combinedFilter = filter != null
                ? Builders<T>.Filter.And(activeFilter, Builders<T>.Filter.Where(filter))
                : activeFilter;

            return await _context.GetCollection<T>(typeof(T).Name)
                .Find(combinedFilter)
                .ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> filter)
        {
            var collection = _context.GetCollection<T>(typeof(T).Name);
            return await collection.Find(filter).ToListAsync();
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> filter)
        {
            var collection = _context.GetCollection<T>(typeof(T).Name);
            return await collection.Find(filter).AnyAsync();
        }

        public async Task<T> GetByIdAsync(ObjectId id)
        {
            return await _context.GetCollection<T>(typeof(T).Name).Find(e => e.Id == id).FirstOrDefaultAsync();
        }

        public async Task<T> GetByIdAsync(string id)
        {
            if (!ObjectId.TryParse(id, out ObjectId objectId))
            {
                throw new ArgumentException("Format Hatası");
            }
            return await GetByIdAsync(objectId);
        }

        public async Task<OperationResult> AddAsync(T entity)
        {
            try
            {
                if (entity.ObjectStatus == 0)
                {
                    entity.ObjectStatus = 1;
                }
                await _context.GetCollection<T>(typeof(T).Name).InsertOneAsync(entity);
                return new OperationResult
                {
                    IsValid = true,
                    Messages = new List<string>() { "İşlem Başarılı" },
                    Data = entity
                };
            }
            catch (Exception ex)
            {
                return new OperationResult()
                {
                    IsValid = false,
                    Messages = new List<string>() { ex.Message },
                };
            }
           
        }

        public async Task UpdateAsync(ObjectId id, T entity)
        {
            await _context.GetCollection<T>(typeof(T).Name).ReplaceOneAsync(e => e.Id == id, entity);
        }

        public async Task DeleteAsync(ObjectId id)
        {
            await _context.GetCollection<T>(typeof(T).Name).DeleteOneAsync(e => e.Id == id);
        }
    }
}
