using System;
using Bacola.Core.Entities.BaseEntities;
using System.Linq.Expressions;

namespace Bacola.Core.Repositories.Generic
{
    public interface IRepository<T> where T : BaseEntity
    {
        public Task AddAsync(T entity);
        public Task UpdateAsync(T entity);
        public Task<T> GetAsync(Expression<Func<T, bool>> expression, params string[] Includes);
        public IQueryable<T> GetQuery(Expression<Func<T, bool>> expression);
        public Task RemoveAsync(T entity);
        public int SaveChanges();
        public Task<int> SaveChangesAsync();
    }
}

