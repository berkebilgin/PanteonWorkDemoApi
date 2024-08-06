using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using CrossCutting.CoreModels;
using CrossCutting.Enums;
using EntityLayer.PanteonEntity.MsEntity.BaseEntity;

namespace DataLayer.Repository.MsRepository.BaseRepository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntityModel
    {
        private readonly ApplicationMsDbContext _context;
        private readonly DbSet<T> _dbSet;

        protected BaseRepository()
        {
            _context = new ApplicationMsDbContext();
            _dbSet = _context.Set<T>();
        }

        public async Task<T?> GetActiveFirstOrDefaultAsync(Expression<Func<T, bool>>? predicate = null)
        {
            IQueryable<T> query = _dbSet.Where(e => e.ObjectStatus == (int)ObjectStatus.Active);
            if (predicate != null)
            {
                query = query.Where(predicate);
            }
            return await query.FirstOrDefaultAsync();
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.AnyAsync(predicate);
        }

        public async Task<OperationResult> AddAsync(T entity)
        {
            try
            {
                if (entity.ObjectStatus == 0)
                {
                    entity.ObjectStatus = 1;
                }
                await _dbSet.AddAsync(entity);
                await _context.SaveChangesAsync();

                return new OperationResult
                {
                    IsValid = true,
                    Messages= new List<string>(){ "İşlem Başarılı" },
                    Data = entity
                };
            }
            catch (Exception)
            {
                return new OperationResult
                {
                    IsValid = false,
                    Messages = new List<string>() { "İşlem Sırasında Bir Hata Oluştu"},
                };
            }
        }


        public async Task<OperationResult> UpdateAsync(T entity)
        {
            try
            {
                var existingEntity = await _dbSet.FindAsync(entity.Id);
                if (existingEntity == null)
                {
                    return new OperationResult
                    {
                        IsValid = false,
                        Messages = new List<string>() { "İşlem Sırasında Bir Hata Oluştu" },
                    };
                }

                _context.Entry(existingEntity).CurrentValues.SetValues(entity);
                await _context.SaveChangesAsync();

                return new OperationResult
                {
                    IsValid = true,
                    Messages = new List<string>() { "İşlem Başarılı" },
                    Data = entity
                };
            }
            catch (Exception)
            {
                return new OperationResult
                {
                    IsValid = false,
                    Messages = new List<string>() { "İşlem Sırasında Bir Hata Oluştu" },
                };
            }
        }
    }
}