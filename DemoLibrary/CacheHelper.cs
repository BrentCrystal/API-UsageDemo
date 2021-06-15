using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace DemoLibrary
{
    public static class CacheHelper<TItem>
    {
        private static MemoryCache _cache = new MemoryCache(new MemoryCacheOptions());
        private static ConcurrentDictionary<object, SemaphoreSlim> _locks = new ConcurrentDictionary<object, SemaphoreSlim>();

        public static async Task<TItem> GetOrCreate(object key, Func<Task<TItem>> createItem)
        {
            TItem cacheEntry;

            if (!_cache.TryGetValue(key, out cacheEntry))
            {
                SemaphoreSlim myLock = _locks.GetOrAdd(key, k => new SemaphoreSlim(1, 1));
                await myLock.WaitAsync();

                try
                {
                    if (!_cache.TryGetValue(key, out cacheEntry))
                    {
                        //Key not in cache, so get data
                        cacheEntry = await createItem();
                        
                        //Save Data in cache
                        _cache.Set(key, cacheEntry);
                    }
                }
                finally
                {
                    myLock.Release();
                }
            }
            return cacheEntry;
        }
    }
}
