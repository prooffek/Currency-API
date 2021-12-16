using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AveneoRerutacja.ApiHandler;
using Microsoft.EntityFrameworkCore;

namespace AveneoRerutacja.Infrastructure
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly DbContext _context;
        private readonly DbSet<T> _db;

        
        public GenericRepository(DbContext context)
        {
            _context = context;
            _db = _context.Set<T>();
        }
        
        
        public async Task<IList<T>> GetAll(IList<string> includes)
        {
            IQueryable<T> query = _db;

            query = GetIncludes(query, includes);

            return await query.ToListAsync();


        }

        
        public async Task<IList<T>> GetAll(Expression<Func<T, bool>> expression, IList<string> includes)
        {
            IQueryable<T> query = _db;

            query = GetByExpression(query, expression);
            query = GetIncludes(query, includes);

            return await query.ToListAsync();
        }

        
        public async Task<IList<T>> GetAll(Expression<Func<T, bool>> expression = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, IList<string> includes = null)
        {
            IQueryable<T> query = _db;

            query = GetByExpression(query, expression);
            query = GetIncludes(query, includes);
            query = GetOrderedQuery(query, orderBy);

            return await query.ToListAsync();
        }


        public async Task<T> Get(Expression<Func<T, bool>> expression = null, List<string> includes = null)
        {
            IQueryable<T> query = _db;
            
            query = GetIncludes(query, includes);

            return await query.FirstOrDefaultAsync(expression);
        }

        
        public async Task Add(T entity)
        {
            await _db.AddAsync(entity);
        }


        public async Task AddRange(IEnumerable<T> entities)
        {
            await _db.AddRangeAsync(entities);
        }

        
        private IQueryable<T> GetIncludes(IQueryable<T> query, IList<string> includes)
        {
            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            return query;
        }


        private IQueryable<T> GetByExpression(IQueryable<T> query, Expression<Func<T, bool>> expression)
        {
            if (expression != null)
            {
                query = query.Where(expression);
            }

            return query;
        }


        private IQueryable<T> GetOrderedQuery(IQueryable<T> query, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy)
        {
            if (orderBy != null)
            {
                query = orderBy(query);
            }

            return query;
        }
    }
}