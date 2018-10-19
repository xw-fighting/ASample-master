using System;
using System.Collections.Concurrent;

namespace ASample.ExtensionMethod.Cache
{
    internal static class CacheContainerManager<TKey, TObject>
    {
        internal static ConcurrentDictionary<Guid, ConcurrentDictionary<TKey, TObject>> Containers =
            new ConcurrentDictionary<Guid, ConcurrentDictionary<TKey, TObject>>();
    }

    public static class CacheContainerManager
    {
        public static ConcurrentDictionary<TKey, TObject> Instance<TKey, TObject>(Guid id = default(Guid))
        {
            return CacheContainerManager<TKey, TObject>.Containers.GetOrAdd(id,
                i => new ConcurrentDictionary<TKey, TObject>());
        }

        public static ConcurrentDictionary<Type, T> ReflectionCacheContainer<T>(Guid id = default(Guid))
        {
            return Instance<Type, T>(id);
        }
    }
}
