using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RepositoryPatternWithUOW.Core.Consts;
using RepositoryPatternWithUOW.Core.Interfaces;

namespace RepositoryPatternWithUOW.EF.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected ApplicationDbContext _context;
        public BaseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        #region GetAll
        public IEnumerable<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }
        #endregion
        #region GetById
        public T GetById(int id)
        {
            return _context.Set<T>().Find(id);
        }
        public async Task<T> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }
        #endregion
        #region Find With criteria
        public T Find(Expression<Func<T, bool>> criteria, string[] includes = null)
        {
            IQueryable<T> query = _context.Set<T>();
            if (includes != null)
                foreach(string include in includes)
                    query = query.Include(include);

            return query.SingleOrDefault(criteria);
        }
        public async Task<T> FindAsync(Expression<Func<T, bool>> criteria, string[] includes = null)
        {
            IQueryable<T> query = _context.Set<T>();
            if (includes != null)
                foreach (string include in includes)
                    query = query.Include(include);

            return await query.SingleOrDefaultAsync(criteria);
        }
        #endregion
        #region FindAll With criteria
        public IEnumerable<T> FindAll(Expression<Func<T, bool>> criteria, string[] includes = null)
        {
            IQueryable<T> query = _context.Set<T>();
            if (includes != null)
                foreach (string include in includes)
                    query = query.Include(include);

            return query.Where(criteria).ToList();
        }
        public async Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> criteria, string[] includes = null)
        {
            IQueryable<T> query = _context.Set<T>();
            if (includes != null)
                foreach (string include in includes)
                    query = query.Include(include);

            return await query.Where(criteria).ToListAsync();
        }
        #endregion
        #region FindAll overload
        public IEnumerable<T> FindAll(Expression<Func<T, bool>> criteria, int? take, int? skip, 
            Expression<Func<T, object>> orederBy = null, string orderByDirection = OrderBy.Ascending)
        {
            IQueryable<T> query = _context.Set<T>().Where(criteria);

            if(take.HasValue)
                query = query.Take(take.Value);
            if(skip.HasValue)
                query = query.Skip(skip.Value);

            if (orederBy != null)
            {
                if(orderByDirection == OrderBy.Ascending)
                    query = query.OrderBy(orederBy);
                else
                    query = query.OrderByDescending(orederBy);

            }
            return query.ToList();
        }
        public async Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> criteria, int? take, int? skip, 
            Expression<Func<T, object>> orederBy = null, string orderByDirection = "ASC")
        {
            IQueryable<T> query = _context.Set<T>().Where(criteria);

            if (take.HasValue)
                query = query.Take(take.Value);
            if (skip.HasValue)
                query = query.Skip(skip.Value);

            if (orederBy != null)
            {
                if (orderByDirection == OrderBy.Ascending)
                    query = query.OrderBy(orederBy);
                else
                    query = query.OrderByDescending(orederBy);

            }
            return await query.ToListAsync();
        }
        #endregion
        #region Add
        public T Add(T entity)
        {
            _context.Set<T>().Add(entity);
            _context.SaveChanges();
            return entity;
        }
        public async Task<T> AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            _context.SaveChanges();
            return entity;
        }
        #endregion
        #region Add Range
        public IEnumerable<T> AddRange(IEnumerable<T> entities)
        {
            _context.Set<T>().AddRange(entities);
            _context.SaveChanges();
            return entities;
        }
        public async Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities)
        {
            await _context.Set<T>().AddRangeAsync(entities);
            _context.SaveChanges();
            return entities;
        }
        #endregion
        #region Update
        public T Update(T entity)
        {
            _context.Update(entity);
            _context.SaveChanges();
            return entity;
        }
        #endregion
        #region Delete
        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
            _context.SaveChanges();
        }
        public void DeleteRange(IEnumerable<T> entities)
        {
            _context.Set<T>().RemoveRange(entities);
            _context.SaveChanges();
        }
        #endregion
        #region Count
        public int Count()
        {
            return _context.Set<T>().Count();
        }
        public async Task<int> CountAsync()
        {
            return await _context.Set<T>().CountAsync();
        }
        #endregion
        #region Count With Criteria
        public int Count(Expression<Func<T, bool>> expression)
        {
            return _context.Set<T>().Count(expression);
        }
        public async Task<int> CountAsync(Expression<Func<T, bool>> expression)
        {
            return await _context.Set<T>().CountAsync(expression);
        }
        #endregion
    }
}
