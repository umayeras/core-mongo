using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using WebApp.Data.Abstract.Factories;
using WebApp.Data.Abstract.Repositories;
using WebApp.Model.Constants;
using WebApp.Model.Entities.Base;
using WebApp.Model.Extensions;

namespace WebApp.Data.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class, IEntity, new()
    {
        private readonly IMongoCollection<TEntity> collection;
        private readonly ILogger<BaseRepository<TEntity>> logger;

        protected BaseRepository(IDbConnectionFactory factory, ILogger<BaseRepository<TEntity>> logger)
        {
            this.logger = logger;
            collection = factory.OpenConnection().GetCollection<TEntity>(GetCollectionName(typeof(TEntity)));
        }

        private static string GetCollectionName(ICustomAttributeProvider documentType)
        {
            return ((BsonCollectionAttribute) documentType.GetCustomAttributes(typeof(BsonCollectionAttribute), true)
                .FirstOrDefault())?.CollectionName;
        }

        private string SetMethodNameForLogMessage(string methodName)
        {
            return $"{GetType().Name}.{methodName}";
        }

        #region get

        public virtual IQueryable<TEntity> AsQueryable()
        {
            return collection.AsQueryable();
        }

        public virtual IEnumerable<TEntity> FilterBy(
            Expression<Func<TEntity, bool>> filterExpression)
        {
            return collection.Find(filterExpression).ToEnumerable();
        }
        
        public virtual TEntity FindOne(Expression<Func<TEntity, bool>> filterExpression)
        {
            return collection.Find(filterExpression).FirstOrDefault();
        }

        public virtual Task<TEntity> FindOneAsync(Expression<Func<TEntity, bool>> filterExpression)
        {
            return Task.Run(() => collection.Find(filterExpression).FirstOrDefaultAsync());
        }

        public virtual TEntity FindById(string id)
        {
            return collection.Find(x => x.Id == id).FirstOrDefault();
        }

        public virtual Task<TEntity> FindByIdAsync(string id)
        {
            return Task.Run(() =>
            {
                return collection.Find(x => x.Id == id).SingleOrDefaultAsync();
            });
        }

        #endregion

        #region insert

        public virtual bool InsertOne(TEntity document)
        {
            try
            {
                collection.InsertOne(document);
                return true;
            }
            catch (Exception ex)
            {
                logger.LogError(
                    SetMethodNameForLogMessage(nameof(InsertOne)),
                    $"{document.GetType().Name} {Messages.AddingFailed}. ResultMessage: {ex.Message}");
                return false;
            }
        }

        public virtual string Insert(TEntity document)
        {
            try
            {
                collection.InsertOne(document);
                return document.Id;
            }
            catch (Exception ex)
            {
                logger.LogError(
                    SetMethodNameForLogMessage(nameof(InsertOne)),
                    $"{document.GetType().Name} {Messages.AddingFailed}. ResultMessage: {ex.Message}");
                return string.Empty;
            }
        }

        public virtual Task InsertOneAsync(TEntity document)
        {
            return Task.Run(() => collection.InsertOneAsync(document));
        }

        public bool InsertMany(ICollection<TEntity> documents)
        {
            try
            {
                collection.InsertMany(documents);
                return true;
            }
            catch (Exception ex)
            {
                logger.LogError(
                    SetMethodNameForLogMessage(nameof(InsertOne)),
                    $"{documents.GetType().Name} {Messages.AddingFailed}. ResultMessage: {ex.Message}");

                return false;
            }
        }

        public virtual async Task InsertManyAsync(ICollection<TEntity> documents)
        {
            await collection.InsertManyAsync(documents);
        }

        #endregion

        #region update

        public bool ReplaceOne(TEntity document)
        {
            try
            {
                var filter = Builders<TEntity>.Filter.Eq(doc => doc.Id, document.Id);
                collection.FindOneAndReplace(filter, document);

                return true;
            }
            catch (Exception ex)
            {
                logger.LogError(
                    SetMethodNameForLogMessage(nameof(InsertOne)),
                    $"{document.GetType().Name} {Messages.UpdatingFailed}. ResultMessage: {ex.Message}");

                return false;
            }
        }

        public virtual async Task ReplaceOneAsync(TEntity document)
        {
            var filter = Builders<TEntity>.Filter.Eq(doc => doc.Id, document.Id);
            await collection.FindOneAndReplaceAsync(filter, document);
        }

        #endregion

        #region delete

        public bool DeleteOne(Expression<Func<TEntity, bool>> filterExpression)
        {
            try
            {
                collection.FindOneAndDelete(filterExpression);

                return true;
            }
            catch (Exception ex)
            {
                logger.LogError(
                    SetMethodNameForLogMessage(nameof(InsertOne)),
                    $"{filterExpression.GetType().Name} {Messages.DeletingFailed}. ResultMessage: {ex.Message}");

                return false;
            }
        }

        public Task DeleteOneAsync(Expression<Func<TEntity, bool>> filterExpression)
        {
            return Task.Run(() => collection.FindOneAndDeleteAsync(filterExpression));
        }

        public bool DeleteById(string id)
        {
            try
            {
                collection.FindOneAndDelete(x => x.Id == id);

                return true;
            }
            catch (Exception ex)
            {
                logger.LogError(
                    SetMethodNameForLogMessage(nameof(InsertOne)),
                    $"{Messages.DeletingFailed} Id={id}. ResultMessage: {ex.Message}");

                return false;
            }
        }

        public Task DeleteByIdAsync(string id)
        {
            return Task.Run(() =>
            {
                collection.FindOneAndDeleteAsync(x => x.Id == id);
            });
        }

        public bool DeleteMany(Expression<Func<TEntity, bool>> filterExpression)
        {
            try
            {
                collection.DeleteMany(filterExpression);

                return true;
            }
            catch (Exception ex)
            {
                logger.LogError(
                    SetMethodNameForLogMessage(nameof(InsertOne)),
                    $"{filterExpression.GetType().Name} {Messages.DeletingFailed}. ResultMessage: {ex.Message}");

                return false;
            }
        }

        public Task DeleteManyAsync(Expression<Func<TEntity, bool>> filterExpression)
        {
            return Task.Run(() => collection.DeleteManyAsync(filterExpression));
        }

        #endregion
    }
}