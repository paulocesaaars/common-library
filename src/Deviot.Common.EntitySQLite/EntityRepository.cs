using Deviot.Common.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Deviot.Common.Deviot.Common.EntitySQLite
{
    public abstract class EntityRepository : IEntityRepository
    {
        #region Attributes
        private readonly DbContext _db;
        private const string GENERIC_ERROR = "Houve um erro na camada de infraestrutura.";
        #endregion

        #region Properties
        public bool IsTransaction { get; private set; }
        #endregion

        #region Constants
        #endregion

        #region Constructors
        protected EntityRepository(DbContext db)
        {
            _db = db;
        }
        #endregion

        #region Methods

        #region Private
        private static IQueryable<T> MakeIncludes<T>(IQueryable<T> query, string[] includes) where T : Entity
        {
            if (includes != null)
            {
                foreach (string include in includes)
                {
                    query = query.Include(include).AsQueryable();
                }
            }

            return query;
        }
        #endregion

        #region Public
        public async Task<T> GetAsync<T>(Guid id) where T : Entity
        {
            try
            {
                return await _db.Set<T>().FindAsync(id);
            }
            catch(Exception exception)
            {
                throw new Exception(GENERIC_ERROR, exception);
            }
        }

        public IQueryable<T> Get<T>(bool noTracking = false) where T : Entity
        {
            try
            {
                if (noTracking)
                    return _db.Set<T>().AsNoTracking();
                else
                    return _db.Set<T>();
            }
            catch (Exception exception)
            {
                throw new Exception(GENERIC_ERROR, exception);
            }
        }

        public IQueryable<T> Get<T>(Expression<Func<T, bool>> expression, bool noTracking = false) where T : Entity
        {
            try
            {
                if (noTracking)
                    return _db.Set<T>().Where(expression).AsNoTracking();
                else
                    return _db.Set<T>().Where(expression);
            }
            catch (Exception exception)
            {
                throw new Exception(GENERIC_ERROR, exception);
            }
            
        }

        public IQueryable<T> Get<T>(int take, int skip, params string[] includes) where T : Entity
        {
            try
            {
                var query = _db.Set<T>().AsQueryable();

                if (includes != null)
                    foreach (string include in includes)
                        query = query.Include(include).AsQueryable();

                return query.Skip(skip).Take(take);
            }
            catch (Exception exception)
            {
                throw new Exception(GENERIC_ERROR, exception);
            }
            
        }

        public IQueryable<T> Get<T>(Expression<Func<T, bool>> expression, bool noTracking = false, params string[] includes) where T : Entity
        {
            try
            {
                if (noTracking)
                {
                    var query = _db.Set<T>().AsNoTracking().AsQueryable();
                    query = MakeIncludes<T>(query, includes);
                    return query.Where(expression);
                }
                else
                {
                    var query = _db.Set<T>().AsQueryable();
                    query = MakeIncludes<T>(query, includes);
                    return query.Where(expression);

                }
            }
            catch (Exception exception)
            {
                throw new Exception(GENERIC_ERROR, exception);
            }
            
        }

        public async Task AddAsync<T>(T entity) where T : Entity
        {
            try
            {
                await _db.Set<T>().AddAsync(entity);

                if (!IsTransaction)
                    await _db.SaveChangesAsync();
            }
            catch (Exception exception)
            {
                throw new Exception(GENERIC_ERROR, exception);
            }
        }

        public async Task AddRangeAsync<T>(IEnumerable<T> entities) where T : Entity
        {
            try
            {
                await _db.Set<T>().AddRangeAsync(entities);

                if (!IsTransaction)
                    await _db.SaveChangesAsync();
            }
            catch (Exception exception)
            {
                throw new Exception(GENERIC_ERROR, exception);
            }
        }

        public async Task EditAsync<T>(T entity) where T : Entity
        {
            try
            {
                _db.Entry(entity).State = EntityState.Modified;

                if (!IsTransaction)
                    await _db.SaveChangesAsync();
            }
            catch (Exception exception)
            {
                throw new Exception(GENERIC_ERROR, exception);
            }
        }

        public async Task EditRangeAsync<T>(IEnumerable<T> entities) where T : Entity
        {
            try
            {
                _db.Entry(entities).State = EntityState.Modified;

                if (!IsTransaction)
                    await _db.SaveChangesAsync();
            }
            catch (Exception exception)
            {
                throw new Exception(GENERIC_ERROR, exception);
            }
        }

        public async Task EditAsync<T>(Guid id, T entity) where T : Entity
        {
            try
            {
                var oldEntity = await GetAsync<T>(id);

                _db.Entry(oldEntity).State = EntityState.Modified;

                _db.Entry(oldEntity).CurrentValues.SetValues(entity);

                if (!IsTransaction)
                    await _db.SaveChangesAsync();
            }
            catch (Exception exception)
            {
                throw new Exception(GENERIC_ERROR, exception);
            }
        }

        public async Task DeleteAsync<T>(Guid id) where T : Entity
        {
            try
            {
                await DeleteAsync(await GetAsync<T>(id));
            }
            catch (Exception exception)
            {
                throw new Exception(GENERIC_ERROR, exception);
            }
        }

        public async Task DeleteAsync<T>(T entity) where T : Entity
        {
            try
            {
                _db.Entry(entity).State = EntityState.Deleted;

                if (!IsTransaction)
                    await _db.SaveChangesAsync();
            }
            catch (Exception exception)
            {
                throw new Exception(GENERIC_ERROR, exception);
            }
        }

        public async Task DeleteRangeAsync<T>(IEnumerable<T> entities) where T : Entity
        {
            try
            {
                _db.RemoveRange(entities);

                if (!IsTransaction)
                    await _db.SaveChangesAsync();
            }
            catch (Exception exception)
            {
                throw new Exception(GENERIC_ERROR, exception);
            }
        }

        public async Task DeleteAsync<T>(Expression<Func<T, bool>> expression, bool shouldDelete = true) where T : Entity
        {
            try
            {
                var entities = await _db.Set<T>().Where(expression).ToListAsync();

                entities?.ForEach(entity =>
                {
                    if (shouldDelete)
                    {

                        _db.Entry(entity).State = EntityState.Deleted;
                    }
                    else
                    {
                        _db.Entry(entity).State = EntityState.Modified;
                    }
                });

                if (!IsTransaction)
                    await _db.SaveChangesAsync();
            }
            catch (Exception exception)
            {
                throw new Exception(GENERIC_ERROR, exception);
            }
        }

        public async Task<int> CountAsync<T>(Expression<Func<T, bool>> expression) where T : Entity
        {
            try
            {
                return await _db.Set<T>().Where(expression).CountAsync();
            }
            catch (Exception exception)
            {
                throw new Exception(GENERIC_ERROR, exception);
            }
        }

        public async Task<bool> ExistsAsync<T>(Expression<Func<T, bool>> expression) where T : Entity
        {
            try
            {
                return await _db.Set<T>().AnyAsync(expression);
            }
            catch (Exception exception)
            {
                throw new Exception(GENERIC_ERROR, exception);
            }
        }

        public async Task<T> FirstAsync<T>(Expression<Func<T, bool>> expression) where T : Entity
        {
            try
            {
                return await _db.Set<T>().FirstOrDefaultAsync(expression);
            }
            catch (Exception exception)
            {
                throw new Exception(GENERIC_ERROR, exception);
            }
        }

        public async Task<T> FirstAsync<T>(Expression<Func<T, bool>> expression, bool noTracking = false) where T : Entity
        {
            try
            {
                if (noTracking)
                {
                    return await _db.Set<T>().AsNoTracking().FirstOrDefaultAsync(expression);
                }
                else
                {
                    return await _db.Set<T>().FirstOrDefaultAsync(expression);
                }
            }
            catch (Exception exception)
            {
                throw new Exception(GENERIC_ERROR, exception);
            }
        }

        public async Task SaveChangesAsync()
        {
            try
            {
                await _db.SaveChangesAsync();
            }
            catch (Exception exception)
            {
                throw new Exception(GENERIC_ERROR, exception);
            }
        }


        public async Task BeginTransactionAsync()
        {
            try
            {
                IsTransaction = true;
                await _db.Database.BeginTransactionAsync();
            }
            catch (Exception exception)
            {
                throw new Exception(GENERIC_ERROR, exception);
            }
        }

        public async Task<bool> CommitTransactionAsync()
        {
            try
            {
                _db.Database.CommitTransaction();
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                RollbackTransaction();
            }

            return false;
        }

        public void RollbackTransaction()
        {
            try
            {
                _db.Database.RollbackTransaction();
            }
            catch (Exception exception)
            {
                throw new Exception(GENERIC_ERROR, exception);
            }
        }

        public void Dispose()
        {
            if (_db != null)
                _db.Dispose();
        }
        #endregion

        #region Protected
        #endregion

        #endregion

        #region Events
        #endregion
    }
}