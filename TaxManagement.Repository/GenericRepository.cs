using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using TaxManagement.Core.Interfaces.Repository;
using TaxManagement.Repository.Interfaces;

namespace TaxManagement.Repository
{
    public  class GenericRepository<TEntity> : IGenericRepository<TEntity>
        where TEntity : class
    {

        protected readonly ITaxDbContext _taxDbContext;

        private readonly DbSet<TEntity> _entityItems;


        public GenericRepository(ITaxDbContext context)
        {
            _taxDbContext = context;
            _entityItems = _taxDbContext.Set<TEntity>();
        }

        public async Task AddAsync(TEntity entity)
        {
            await _entityItems.AddAsync(entity);
        }

        public async Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            await _entityItems.AddRangeAsync(entities);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _entityItems.ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetByAsync(
            Expression<Func<TEntity, bool>> wherePredicate)
        {
            return await _entityItems.Where(wherePredicate).ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await _entityItems.FindAsync(id);
        }

        public virtual async Task SaveChangesAsync()
        {
            await _taxDbContext.SaveChangesAsync(CancellationToken.None);
        }

        public void Update(TEntity entity)
        {
            var entry = _taxDbContext.Entry(entity);
            entry.State = EntityState.Modified;
        }

        public void UpdateRange(IEnumerable<TEntity> entities)
        {
            _entityItems.UpdateRange(entities);
        }

    }
}
