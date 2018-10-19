using ASample.EntityFramework.Domain.Repository;
using System;
using System.Data.Entity;
using System.Runtime.Remoting.Messaging;
using System.Threading.Tasks;

namespace ASample.EntityFramework.Storage.Repository
{
    public class BasicEntityFrameworkRepository<TDbContext,TEntity,TKey> :IBasicEntityFrameworkRepository<TEntity, TKey> 
        where TDbContext: DbContext ,new()
        where TEntity : class
    {
        TDbContext db
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

        public DbSet<TEntity> _dbSet;
        public BasicEntityFrameworkRepository()
        {
            _dbSet = db.Set<TEntity>();
        }

        public async Task AddAsync(TEntity entity)
        {
            _dbSet.Add(entity);
            await db.SaveChangesAsync();
        }

        public async Task UpdateAsync(TEntity entity)
        {
            _dbSet.Attach(entity);
            db.Entry(entity).State = EntityState.Modified;
            await db.SaveChangesAsync();
        }

        public async Task DeleteAsync(TKey key)
        {
            var entity = await GetAsync(key);
            if(entity == null)
                throw new Exception("不存在该记录");
            _dbSet.Remove(entity);
            await db.SaveChangesAsync();
        }



        public async Task<TEntity> GetAsync(TKey key)
        {
            return await _dbSet.FindAsync(key);
        }

        public async Task Commit()
        {
            await db.SaveChangesAsync();
        }
    }
}
