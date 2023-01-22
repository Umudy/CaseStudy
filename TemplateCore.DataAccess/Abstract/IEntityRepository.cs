using TemplateCore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace TemplateCore.DataAccess.Abstract
{
    public interface IEntityRepository<TEntity> where TEntity : class, IEntity
    {
        TEntity Insert(TEntity entity);
        void InsertRange(IEnumerable<TEntity> entities);
        TEntity Update(TEntity entity);
        void Delete(TEntity entity);

        //void DeleteRange(IEnumerable<TEntity> entities);

        IEnumerable<TEntity> GetList(Expression<Func<TEntity, bool>> filter = null);
        IEnumerable<TEntity> GetListOrderByAsc<TKey>(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, TKey>> orderExpression);
        IEnumerable<TEntity> GetListOrderByDesc<TKey>(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, TKey>> orderExpression);
        TEntity Get(Expression<Func<TEntity, bool>> filter, bool asNoTracking = false);
        bool Any(Expression<Func<TEntity, bool>> filter);
        int Count(Expression<Func<TEntity, bool>> filter);
        T Max<T>(Expression<Func<TEntity, T>> source);
        IEnumerable<TKey> Distinct<TKey>(Expression<Func<TEntity, TKey>> source);

        DbSet<TEntity> Entity { get; }
    }
}
