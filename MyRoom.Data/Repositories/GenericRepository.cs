using MyRoom.Data.Contracts;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MyRoom.Data.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity>, IDisposable where TEntity : class
    {
        protected DbSet<TEntity> DbSet;

        private readonly DbContext _dbContext;

        public GenericRepository(DbContext dbContext)
        {
            dbContext.Configuration.LazyLoadingEnabled = false;
            dbContext.Configuration.ProxyCreationEnabled = false;
            _dbContext = dbContext;
            DbSet = _dbContext.Set<TEntity>();
        }

        public GenericRepository()
        {
        }

        public virtual void Update(TEntity entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
        }

        public IQueryable<TEntity> GetAll()
        {
            return DbSet;
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await DbSet.FindAsync(id);
        }

        public virtual TEntity GetById(object id)
        {
            return DbSet.Find(id);
        }


        public IQueryable<TEntity> SearchFor(Expression<Func<TEntity, bool>> predicate)
        {
            return DbSet.Where(predicate);
        }

        public virtual async Task EditAsync(TEntity entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public virtual void Edit(TEntity entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public async Task InsertAsync(TEntity entity)
        {

            DbSet.Add(entity);
            await _dbContext.SaveChangesAsync();
        }

        public void Insert(TEntity entity)
        {
            DbSet.Add(entity);
            _dbContext.SaveChanges();
        }

        public async Task DeleteAsync(TEntity entity)
        {
            DbEntityEntry dbEntityEntry = _dbContext.Entry(entity);
            if (dbEntityEntry.State != EntityState.Deleted)
            {
                dbEntityEntry.State = EntityState.Deleted;
            }
            else
            {
                DbSet.Attach(entity);
                DbSet.Remove(entity);
            }
            await _dbContext.SaveChangesAsync();

        }

        public virtual async Task DeleteAsync(int id)
        {
            var entity = GetById(id);
            if (entity == null) return; // not found; assume already deleted.
            await DeleteAsync(entity);
        }

        public virtual void Delete(int id)
        {
            var entity = GetById(id);
            if (entity == null) return; // not found; assume already deleted.
            Delete(entity);
        }

        public virtual void Delete(TEntity entity)
        {
            DbEntityEntry dbEntityEntry = _dbContext.Entry(entity);
            if (dbEntityEntry.State != EntityState.Deleted)
            {
                dbEntityEntry.State = EntityState.Deleted;
            }
            else
            {
                DbSet.Attach(entity);
                DbSet.Remove(entity);
            }
        }


        public void DeleteCollection(List<TEntity> entities)
        {
            entities.ForEach(delegate(TEntity entity)
            {
                _dbContext.Entry(entity).State = EntityState.Deleted;
            });

            DbSet.RemoveRange(entities);
            _dbContext.SaveChanges();
        }


        public void Dispose(bool disposing)
        {
            if (_dbContext != null)
            {
                _dbContext.Dispose();
            }
            GC.SuppressFinalize(this);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}