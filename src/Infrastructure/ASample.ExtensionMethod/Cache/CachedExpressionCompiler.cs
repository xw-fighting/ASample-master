using System;
using System.Collections.Concurrent;

namespace ASample.ExtensionMethod.Cache
{
    /// <summary>
    /// 表达式编译器，该抽象类为编译后的表达式树提供了一个缓存块
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    public abstract class CachedExpressionCompiler<TKey, TResult> : ICacheBlock<TKey, TResult>
    {
        protected CachedExpressionCompiler(Guid blockId)
        {
            _id = blockId;
        }

        protected CachedExpressionCompiler()
        {
            _id = Guid.NewGuid();
        }

        private readonly Guid _id;

        protected virtual Guid Id
        {
            get { return _id; }
        }

        protected ConcurrentDictionary<TKey, TResult> ConcurrentDic
        {
            get { return CacheContainerManager.Instance<TKey, TResult>(Id); }
        }

        public TResult GetOrAdd(TKey key, Func<TKey, TResult> valueProvider)
        {
            return ConcurrentDic.GetOrAdd(key, valueProvider);
        }
    }
}
