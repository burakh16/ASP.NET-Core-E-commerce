using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ecommerce.Models.Services
{
    public interface IRepository<T> where T : class
    {
        DbSet<T> Table { get; }
        T Get(Expression<Func<T, bool>> id);
        T FindBy(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);
        bool Add(T entity);
        bool Update(T entity);
        bool Delete(T entity);


        IQueryable<T> All();
        IQueryable<T> Where(Expression<Func<T, bool>> where);
        IQueryable<T> OrderBy<TKey>(Expression<Func<T, TKey>> orderBy, bool isDesc);
    }
}