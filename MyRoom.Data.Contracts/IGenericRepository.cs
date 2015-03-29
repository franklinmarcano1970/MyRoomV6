using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web;

namespace MyRoom.Data.Contracts
{
    public interface IGenericRepository<TEntity>
    {
        Task<TEntity> GetByIdAsync(int id);

        IQueryable<TEntity> SearchFor(Expression<Func<TEntity, bool>> predicate);

        IQueryable<TEntity> GetAll();

        Task EditAsync(TEntity entity);

        Task InsertAsync(TEntity entity);

        Task DeleteAsync(TEntity entity);

        Task DeleteAsync(int id);


        void Update(TEntity entity);

        void Delete(TEntity entity);

        void Delete(int id);
    }
}