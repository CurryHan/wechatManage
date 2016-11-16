using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq.Expressions;

namespace Sensing.Data.Infrastructure
{
    public interface IRepository<T> where T : class
    {
        T Add(T entity);
        void Update(T entity);
        void Update(T entity, params Expression<Func<T, object>>[] properties);
        void Delete(T entity);
        void Delete(Expression<Func<T, bool>> where);
        //T GetById(long id);
        T GetById(int id);
        T GetById(string id);
        T Get(Expression<Func<T, bool>> where = null);
        IEnumerable<T> GetAll();
        IEnumerable<T> GetMany(Expression<Func<T, bool>> where);
        //IPagedList<T> GetPage<TOrder>(Page page, Expression<Func<T, bool>> where, Expression<Func<T, TOrder>> order);
        IDbSet<T> DbSet { get; }
    }
}
