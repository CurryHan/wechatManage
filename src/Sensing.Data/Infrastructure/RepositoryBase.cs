﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace Sensing.Data.Infrastructure
{
    public abstract class RepositoryBase<T> where T : class
    {
        private SensingSiteDbContext dataContext;
        protected readonly IDbSet<T> dbset;
        protected RepositoryBase(IDatabaseFactory databaseFactory)
        {
            DatabaseFactory = databaseFactory;
            dbset = DataContext.Set<T>();
        }

        public IDbSet<T> DbSet
        {
            get { return dbset; }
        }

        protected IDatabaseFactory DatabaseFactory
        {
            get;
            private set;
        }

        protected SensingSiteDbContext DataContext
        {
            get { return dataContext ?? (dataContext = DatabaseFactory.Get()); }
        }
        public virtual T Add(T entity)
        {
            return dbset.Add(entity);
        }
        public virtual void Update(T entity)
        {
            dbset.Attach(entity);
            dataContext.Entry(entity).State = EntityState.Modified;

        }
        public virtual void Update(T entity, params Expression<Func<T, object>>[] properties)
        {
            dbset.Attach(entity);
            foreach (var property in properties)
            {
                dataContext.Entry(entity).Property(property).IsModified = true;
            }
        }

        public virtual void Delete(T entity)
        {
            dbset.Remove(entity);
        }
        public virtual void Delete(Expression<Func<T, bool>> where)
        {
            IEnumerable<T> objects = dbset.Where<T>(where).AsEnumerable();
            foreach (T obj in objects)
                dbset.Remove(obj);
        }
        public virtual T GetById(int id)
        {
            return dbset.Find(id);
        }
        public virtual T GetById(string id)
        {
            return dbset.Find(id);
        }
        public virtual IEnumerable<T> GetAll()
        {
            return dbset.ToList();
        }

        public virtual IEnumerable<T> GetMany(Expression<Func<T, bool>> where = null)
        {
            return dbset.Where(where).ToList();
        }


        /// <summary>
        /// Return a paged list of entities
        /// </summary>
        /// <typeparam name="TOrder"></typeparam>
        /// <param name="page">Which page to retrieve</param>
        /// <param name="where">Where clause to apply</param>
        /// <param name="order">Order by to apply</param>
        /// <returns></returns>
        //public virtual IPagedList<T> GetPage<TOrder>(Page page, Expression<Func<T, bool>> where, Expression<Func<T, TOrder>> order)
        //{
        //    var results = dbset.OrderBy(order).Where(where).GetPage(page).ToList();
        //    var total = dbset.Count(where);
        //    return new StaticPagedList<T>(results, page.PageNumber, page.PageSize, total);
        //}

        public T Get(Expression<Func<T, bool>> where)
        {
            if(where == null)
            {
                return dbset.FirstOrDefault();
            }
            return dbset.Where(where).FirstOrDefault<T>();
        }
    }
}
