using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using RepositoryPatternWithUOW.Core.Consts;

namespace RepositoryPatternWithUOW.Core.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        #region GetAll
        IEnumerable<T> GetAll();
        Task<IEnumerable<T>> GetAllAsync();
        #endregion

        #region GetById
        T GetById(int id);
        Task<T> GetByIdAsync(int id);
        #endregion

        #region Find With Criteria
        T Find(Expression<Func<T, bool>> criteria, string[] includes = null);
        Task<T> FindAsync(Expression<Func<T, bool>> criteria, string[] includes = null);
        #endregion

        #region FindAll With criteria
        IEnumerable<T> FindAll(Expression<Func<T, bool>> criteria, string[] includes = null);
        Task<IEnumerable<T>>  FindAllAsync(Expression<Func<T, bool>> criteria, string[] includes = null);
        #endregion

        #region FindAll Overload
        IEnumerable<T> FindAll(Expression<Func<T, bool>> criteria, int? take, int? skip, 
            Expression<Func<T, object>> orederBy = null, string orderByDirection = OrderBy.Ascending);
        Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> criteria, int? take, int? skip,
            Expression<Func<T, object>> orederBy = null, string orderByDirection = OrderBy.Ascending);
        #endregion

        #region Add
        T Add(T entity);
        Task<T> AddAsync(T entity);
        #endregion

        #region Add Range
        IEnumerable<T> AddRange(IEnumerable<T> entities);
        Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities);
        #endregion

        #region Update
        T Update(T entity);
        #endregion

        #region Delete
        void Delete(T entity);
        void DeleteRange(IEnumerable<T> entities);
        #endregion

        #region Count
        int Count();
        Task<int> CountAsync();
        #endregion

        #region Count With criteria
        int Count(Expression<Func<T, bool>> criteria);
        Task<int> CountAsync(Expression<Func<T, bool>> expression);
        #endregion

    }
}
