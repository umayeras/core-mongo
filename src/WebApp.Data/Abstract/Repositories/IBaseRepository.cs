using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WebApp.Model.Entities.Base;

namespace WebApp.Data.Abstract.Repositories
{
    public interface IBaseRepository<T> where T : class, IEntity, new()
    {
        IQueryable<T> AsQueryable();
        IEnumerable<T> FilterBy(Expression<Func<T, bool>> filterExpression);
        T FindOne(Expression<Func<T, bool>> filterExpression);
        Task<T> FindOneAsync(Expression<Func<T, bool>> filterExpression);
        T FindById(string id);
        Task<T> FindByIdAsync(string id);
        bool InsertOne(T document);
        string Insert(T document);
        Task InsertOneAsync(T document);
        bool InsertMany(ICollection<T> documents);
        Task InsertManyAsync(ICollection<T> documents);
        bool ReplaceOne(T document);
        Task ReplaceOneAsync(T document);
        bool DeleteOne(Expression<Func<T, bool>> filterExpression);
        Task DeleteOneAsync(Expression<Func<T, bool>> filterExpression);
        bool DeleteById(string id);
        Task DeleteByIdAsync(string id);
        bool DeleteMany(Expression<Func<T, bool>> filterExpression);
        Task DeleteManyAsync(Expression<Func<T, bool>> filterExpression);
    }
}