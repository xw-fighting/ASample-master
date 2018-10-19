using ASample.EntityFramework.Domain.QueryEntry;
using ASmaple.Domain.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Remoting.Messaging;
using System.Threading.Tasks;

namespace ASample.EntityFramework.Storage.QueryEntry
{
    public class BasicEntityFrameworkQueryEntry<TDbContext, TEntity, TKey> : IBasicEntityFrameworkQueryEntry<TEntity, TKey>
        where TDbContext : DbContext,new()
        where TEntity : AggregateRoot<TKey>
    {

        //保证EF上下文在线程内唯一
        public TDbContext db
        {
            get
            {
                //1.0先从线程缓存中根据key查找EF容器对象，如果没有则创建，同时保存到缓存中，
                object obj = CallContext.GetData(typeof(TDbContext).Name);
                if (obj == null)
                {
                    //实例化EF的上下文容器对象
                    obj = new TDbContext();
                    CallContext.SetData(typeof(TDbContext).Name, obj);
                }
                return obj as TDbContext;

            }
        }
        protected IQueryable<TEntity> DbSet
        {
            get { return Set.AsNoTracking(); }
        }

        public  DbSet<TEntity> Set
        {
            get { return db.Set<TEntity>(); }
        }
        
        public virtual async Task<TEntity> GetAsync(TKey key)
        {
            var result = await Set.FindAsync(key);
            if (result != null)
                return result;
            throw new Exception("未找到该实体");
        }

        public virtual async Task<IList<TEntity>> SelectAsync(Expression<Func<TEntity, bool>> whereLambda = null)
        {
            IList<TEntity> list;
            if (whereLambda == null)
            {
                list = await  DbSet.ToListAsync();
                return list;
            }
            list = await DbSet.Where(whereLambda).ToListAsync();
            return list;
        }
    }
}
