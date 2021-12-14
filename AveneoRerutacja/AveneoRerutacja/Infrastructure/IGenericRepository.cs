using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AveneoRerutacja.Infrastructure
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IList<T>> GetAll(IList<string> includes);
        Task<IList<T>> GetAll(Expression<Func<T, bool>> expression, IList<string> includes);
        Task<IList<T>> GetAll(Expression<Func<T, bool>> expression = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, IList<string> includes = null);
        Task Add(T entity);
        Task AddRange(IEnumerable<T> entities);
    }
}