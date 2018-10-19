using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASample.ExtensionMethod.Cache
{
    public interface ICacheBlock<TKey, TValue>
    {
        TValue GetOrAdd(TKey key, Func<TKey, TValue> valueProvider);
    }
}
