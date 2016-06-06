﻿namespace Epica.Data.Repositories
{
    using System.Linq;

    public interface IRepository<TEntity>
    {
        IQueryable<TEntity> All();

        TEntity FindById(object id);

        void Add(TEntity entity);

        void Remove(TEntity entity);

        void Update(TEntity entity);

        void SaveChanges();
    }
}