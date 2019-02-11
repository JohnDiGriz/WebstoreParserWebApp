using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Linq.Expressions;

namespace ParserWebApp.DAL.Repositories
{
    public class Repository<TEntity> : Interfaces.IRepository<TEntity> where TEntity : Models.BaseModel
    {
        protected readonly DbContext Context_;
        public Repository(DbContext context) {
            Context_ = context;
        }

        public void Add(TEntity entity)
        {
            Context_.Set<TEntity>().Add(entity);
        }
        public void AddRange(IEnumerable<TEntity> entities)
        {
            Context_.Set<TEntity>().AddRange(entities);
        }

        public TEntity Get(int id)
        {
            return Context_.Set<TEntity>().Find(id);
        }
        public IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> predicate)
        {
            return Context_.Set<TEntity>().Where(predicate);
        }
        public IEnumerable<TEntity> GetAll()
        {
            return Context_.Set<TEntity>().ToList();
        }

        public void Remove(TEntity entity)
        {
            Context_.Set<TEntity>().Remove(entity);
        }
        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            Context_.Set<TEntity>().RemoveRange(entities);
        }
    }
}
