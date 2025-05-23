﻿using STCS.Infrastructure.Entities;
using System.Linq.Expressions;

namespace STCS.Infrastructure.Repositories
{
    public interface IRepository<TEntity, TKey> where TEntity : class, IEntity<TKey>
    {
        void Add(TEntity entity);

        void Remove(TKey id);

        void Remove(TEntity entityToDelete);

        void Remove(Expression<Func<TEntity, bool>> filter);

        void Edit(TEntity entityToUpdate);

        int GetCount(Expression<Func<TEntity, bool>> filter = null);

        IList<TEntity> Get(Expression<Func<TEntity, bool>> filter, string includeProperties = "");

        IList<TEntity> GetAll();

        TEntity GetById(TKey id);

        (IList<TEntity> data, int total, int totalDisplay) Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "", int pageIndex = 1, int pageSize = 10, bool isTrackingOff = false);

        (IList<TEntity> data, int total, int totalDisplay) GetDynamic(
            Expression<Func<TEntity, bool>> filter = null,
            string orderBy = null,
            string includeProperties = "", int pageIndex = 1, int pageSize = 10, bool isTrackingOff = false);

        IList<TEntity> Get(Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "", bool isTrackingOff = false);

        IList<TEntity> GetDynamic(Expression<Func<TEntity, bool>> filter = null,
            string orderBy = null,
            string includeProperties = "", bool isTrackingOff = false);
    }
}