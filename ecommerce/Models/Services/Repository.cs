using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using ecommerce.Data;
using Microsoft.EntityFrameworkCore;

namespace ecommerce.Models.Services
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext dbContext;

        public Repository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
            this.Table = dbContext.Set<T>();
        }

        public DbSet<T> Table { get; set; }

        public bool Add(T entity)
        {
            Table.Add(entity);
            return Save();
        }

        public T FindBy(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
        {
            var query = All().Where(predicate);
            return includes.Aggregate(query, (current, includeProperty) => current.Include(includeProperty)).SingleOrDefault();
        }
        public T Get(Expression<Func<T, bool>> id)
        {
            return Table.SingleOrDefault(id);
        }

        public bool Update(T entity)
        {
            Table.Update(entity);
            return Save();
        }

        public bool Delete(T entity)
        {
            Table.Remove(entity);
            return Save();
        }

        public IQueryable<T> All()
        {
            return Table;
        }

        public IQueryable<T> Where(Expression<Func<T, bool>> where)
        {
            return Table.Where(where);
        }

        public IQueryable<T> OrderBy<TKey>(Expression<Func<T, TKey>> orderBy, bool isDesc)
        {
            if (isDesc)
                return Table.OrderByDescending(orderBy);
            return Table.OrderBy(orderBy);
        }


        private bool Save()
        {
            try
            {
                dbContext.SaveChanges();
                return true;
            }
            catch
            {
                // TODO: Log Exceptions
                return false;
            }
        }
    }
}