using ASample.ThirdParty.MongoDb.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace ASample.ThirdParty.MongoDb.Base
{
    public class BaseMongoDbRepository<TMongoClient, TEntity, TKey>  : IMongoDbRepository<TEntity>
        where TMongoClient : MongoClient, new()
        where TEntity :Entity
        
    {
        TMongoClient client
        {
            get
            {
                //1.0先从线程缓存中根据key查找EF容器对象，如果没有则创建，同时保存到缓存中，
                object obj = CallContext.GetData(typeof(TMongoClient).Name);
                if (obj == null)
                {
                    //实例化EF的上下文容器对象
                    obj = new TMongoClient();
                    CallContext.SetData(typeof(TMongoClient).Name, obj);
                }
                return obj as TMongoClient;
            }
        }
        public MongoCollection<TEntity> _dbSet;


        public IQueryable<TEntity> AsQueryable()
        {
            TDataBase
        }

        public bool Delete(TKey id)
        {
            throw new NotImplementedException();
        }

        public TEntity Get(TKey id)
        {
            throw new NotImplementedException();
        }

        public bool Insert(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<bool> InsertBatch(IEnumerable<TEntity> entities)
        {
            throw new NotImplementedException();
        }

        public bool RemoveAll()
        {
            throw new NotImplementedException();
        }

        public bool Save(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public TEntity Get(ObjectId id)
        {
            throw new NotImplementedException();
        }

        public bool Delete(ObjectId id)
        {
            throw new NotImplementedException();
        }
    }

}
