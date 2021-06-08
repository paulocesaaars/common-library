using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Deviot.Common.Deviot.Common.EntitySQLite
{
    public interface IEntityRepository : IDisposable
    {
        Task<TEntity> GetAsync<TEntity>(Guid id) where TEntity : Entity;

        IQueryable<TEntity> Get<TEntity>() where TEntity : Entity;

        IQueryable<TEntity> Get<TEntity>(Expression<Func<TEntity, bool>> expression) where TEntity : Entity;

        Task AddAsync<TEntity>(TEntity entity) where TEntity : Entity;

        Task AddRangeAsync<T>(IEnumerable<T> entities) where T : Entity;

        Task EditAsync<TEntity>(TEntity entity) where TEntity : Entity;

        Task EditRangeAsync<T>(IEnumerable<T> entities) where T : Entity;

        Task EditAsync<TEntity>(Guid id, TEntity entity) where TEntity : Entity;

        Task DeleteAsync<TEntity>(Guid id) where TEntity : Entity;

        Task DeleteAsync<TEntity>(TEntity entity) where TEntity : Entity;

        Task DeleteRangeAsync<T>(IEnumerable<T> entities) where T : Entity;

        Task DeleteAsync<TEntity>(Expression<Func<TEntity, bool>> expression, bool shouldDelete = true) where TEntity : Entity;

        Task<TEntity> FirstAsync<TEntity>(Expression<Func<TEntity, bool>> expression) where TEntity : Entity;

        Task<TEntity> FirstAsync<TEntity>(Expression<Func<TEntity, bool>> expression, bool noTracking = true) where TEntity : Entity;

        Task<bool> ExistsAsync<TEntity>(Expression<Func<TEntity, bool>> expression) where TEntity : Entity;

        Task<int> CountAsync<TEntity>(Expression<Func<TEntity, bool>> expression) where TEntity : Entity;

        Task SaveChangesAsync();

        Task BeginTransactionAsync();

        Task<bool> CommitTransactionAsync();

        void RollbackTransaction();
    }
}