using TemplateCore.DataAccess.Abstract;
using TemplateCore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace TemplateCore.DataAccess.Concrete
{
    public abstract class EntityRepositoryBase<TEntity> : IEntityRepository<TEntity>
        where TEntity : class, IEntity
    {
        private readonly DbContext _dbContext;

        protected EntityRepositoryBase(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        protected virtual TEntity InsertContext(DbContext context, TEntity entity)
        {
            var dbEntry = context.Add(entity);
            context.SaveChanges();
            return dbEntry.Entity;
        }

        protected virtual TEntity UpdateContext(DbContext context, TEntity entity)
        {
            context.Update(entity);
            context.SaveChanges();
            return entity;
        }

        public TEntity Insert(TEntity entity)
        {
            return InsertContext(_dbContext, entity);
        }

        public void InsertRange(IEnumerable<TEntity> entities)
        {
            _dbContext.AddRange(entities);
            _dbContext.SaveChanges();
        }

        public TEntity Update(TEntity entity)
        {
            return UpdateContext(_dbContext, entity);
        }

        //public void  DeleteRange(IEnumerable<TEntity> entities)
        //{
        //    foreach (var entity in entities)
        //    {
               
        //        Delete(entity);
        //    }
        //    //_dbContext.RemoveRange(entities);
        //    //_dbContext.SaveChanges();
        //}

        public void Delete(TEntity entity)
        {
            _dbContext.Remove(entity);
            _dbContext.SaveChanges();
        }

        public TEntity Get(Expression<Func<TEntity, bool>> filter, bool asNoTracking = false)
        {
            if (asNoTracking)
                return _dbContext.Set<TEntity>().AsNoTracking().SingleOrDefault(filter);

            return _dbContext.Set<TEntity>().SingleOrDefault(filter);
        }

        public IEnumerable<TEntity> GetList(Expression<Func<TEntity, bool>> filter = null)
        {
            if (filter == null)
                return _dbContext.Set<TEntity>().ToList();

            return _dbContext.Set<TEntity>().Where(filter).ToList();
        }

        public IEnumerable<TEntity> GetListOrderByAsc<TKey>(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, TKey>> orderExpression)
        {
            return _dbContext.Set<TEntity>().Where(filter).OrderBy(orderExpression).ToList();
        }

        public IEnumerable<TEntity> GetListOrderByDesc<TKey>(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, TKey>> orderExpression)
        {
            return _dbContext.Set<TEntity>().Where(filter).OrderByDescending(orderExpression).ToList();
        }

        public bool Any(Expression<Func<TEntity, bool>> filter)
        {
            return _dbContext.Set<TEntity>().Any(filter);
        }

        public int Count(Expression<Func<TEntity, bool>> filter)
        {
            return filter == null
                ? _dbContext.Set<TEntity>().Count()
                : _dbContext.Set<TEntity>().Count(filter);
        }

        public T Max<T>(Expression<Func<TEntity, T>> source)
        {
            return _dbContext.Set<TEntity>().Max(source);
        }

        public IEnumerable<TKey> Distinct<TKey>(Expression<Func<TEntity, TKey>> source)
        {
            return _dbContext.Set<TEntity>().GroupBy(source).Select(t => t.Key);
        }

        public DbSet<TEntity> Entity => _dbContext.Set<TEntity>();
    }
}
