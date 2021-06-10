using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Deviot.Common.Deviot.Common.EntitySQLite
{
    public abstract class EntityRepository : IEntityRepository
    {
        #region Attributes
        private const string GENERIC_ERROR = "Houve um erro na camada de infraestrutura.";
        #endregion

        #region Properties
        public DbContext DbContext { get; private set; }
        #endregion

        #region Constants
        #endregion

        #region Constructors
        protected EntityRepository(DbContext db)
        {
            DbContext = db;
        }
        #endregion

        #region Methods

        #region Private
        #endregion

        #region Public
        public IQueryable<TEntity> Get<TEntity>() where TEntity : Entity
        {
            try
            {
                return DbContext.Set<TEntity>();
            }
            catch (Exception exception)
            {
                throw new Exception(GENERIC_ERROR, exception);
            }
        }

        public async Task AddAsync<TEntity>(TEntity entity) where TEntity : Entity
        {
            try
            {
                await DbContext.Set<TEntity>().AddAsync(entity);
                await DbContext.SaveChangesAsync();
            }
            catch (Exception exception)
            {
                throw new Exception(GENERIC_ERROR, exception);
            }
        }

        public async Task EditAsync<TEntity>(TEntity entity) where TEntity : Entity
        {
            try
            {
                DbContext.Entry(entity).State = EntityState.Modified;
                await DbContext.SaveChangesAsync();
            }
            catch (Exception exception)
            {
                throw new Exception(GENERIC_ERROR, exception);
            }
        }

        public async Task DeleteAsync<TEntity>(TEntity entity) where TEntity : Entity
        {
            try
            {
                DbContext.Entry(entity).State = EntityState.Deleted;
                await DbContext.SaveChangesAsync();
            }
            catch (Exception exception)
            {
                throw new Exception(GENERIC_ERROR, exception);
            }
        }

        public void Dispose()
        {
            if (DbContext != null)
                DbContext.Dispose();
        }
        #endregion

        #region Protected
        #endregion

        #endregion

        #region Events
        #endregion
    }
}