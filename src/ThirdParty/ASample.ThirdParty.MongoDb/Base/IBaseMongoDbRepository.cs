using ASample.ThirdParty.MongoDb.Models;
using MongoDB.Bson;
using System.Collections.Generic;
using System.Linq;

namespace ASample.ThirdParty.MongoDb.Base
{
    public interface IBaseMongoDbRepository<TEntity,TKey> where TEntity : class
    {
        IEnumerable<bool> InsertBatch(IEnumerable<TEntity> entities);
        bool Insert(TEntity entity);
        TEntity Get(TKey id);
        bool Save(TEntity entity);
        bool Delete(TKey id);
        IQueryable<TEntity> AsQueryable();
        bool RemoveAll();
    }

    public interface IMongoDbRepository<T> : IBaseMongoDbRepository<T, ObjectId> where T : Entity
    {

    }
}
