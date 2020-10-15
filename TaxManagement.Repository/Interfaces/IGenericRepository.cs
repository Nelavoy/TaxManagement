using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace TaxManagement.Repository.Interfaces
{
    public interface IGenericRepository<TEntity>
    where TEntity : class
    {
        Task AddAsync(TEntity entity);

        Task AddRangeAsync(IEnumerable<TEntity> entities);

        Task<IEnumerable<TEntity>> GetAllAsync();

        Task<IEnumerable<TEntity>> GetByAsync(
            Expression<Func<TEntity, bool>> wherePredicate);

        Task<TEntity> GetByIdAsync(int id);

        Task SaveChangesAsync();

        void Update(TEntity entity);

        void UpdateRange(IEnumerable<TEntity> entities);

    }
}
