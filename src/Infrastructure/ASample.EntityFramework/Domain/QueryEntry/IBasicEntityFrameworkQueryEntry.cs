
using ASmaple.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ASample.EntityFramework.Domain.QueryEntry
{
    public interface IBasicEntityFrameworkQueryEntry<T,in TKey> where T : AggregateRoot<TKey>
    {
        /// <summary>
        /// 查询所有
        /// </summary>
        /// <returns></returns>
        //Task<IList<T>> SelectAsync(Func<IQueryable<T>, IQueryable<T>> applyExp = null);

        Task<IList<T>> SelectAsync(Expression<Func<T,bool>> whereLambda = null);

        Task<T> GetAsync(TKey id);

        //Task<IPaged<>> SelectPagedAsync();

    }
}
