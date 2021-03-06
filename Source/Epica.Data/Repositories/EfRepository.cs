﻿namespace Epica.Data.Repositories
{
    using System.Linq;
    using System.Data.Entity;
    using Epica.Data;

    public class EfRepository<TEntity> : IRepository<TEntity>
        where TEntity : class
    {
        private readonly IEpicaDbContext data;
        private readonly IDbSet<TEntity> set;

        public EfRepository()
            : this(new EpicaDbContext())
        {
        }

        public EfRepository(IEpicaDbContext data)
        {
            this.data = data;
            this.set = data.Set<TEntity>();
        }

        public IQueryable<TEntity> All()
        {
            return this.set;
        }

        public TEntity FindById(object id)
        {
            return this.set.Find(id);
        }

        public void Add(TEntity entity)
        {
            this.ChangeState(entity, EntityState.Added);
        }

        public void Remove(TEntity entity)
        {
            this.ChangeState(entity, EntityState.Deleted);
        }

        public void Update(TEntity entity)
        {
            this.ChangeState(entity, EntityState.Modified);
        }

        public void SaveChanges()
        {
            this.data.SaveChanges();
        }

        private void ChangeState(TEntity entity, EntityState state)
        {
            var dbEntry = this.data.Entry(entity);
            dbEntry.State = state;
        }
    }
}
