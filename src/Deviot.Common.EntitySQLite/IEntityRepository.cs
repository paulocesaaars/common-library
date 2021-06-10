using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Deviot.Common.Deviot.Common.EntitySQLite
{
    public interface IEntityRepository : IDisposable
    {
        DbContext DbContext { get; }

        IQueryable<TEntity> Get<TEntity>() where TEntity : Entity;

        Task AddAsync<TEntity>(TEntity entity) where TEntity : Entity;

        Task EditAsync<TEntity>(TEntity entity) where TEntity : Entity;

        Task DeleteAsync<TEntity>(TEntity entity) where TEntity : Entity;
    }
}