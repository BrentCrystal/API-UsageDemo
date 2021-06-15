using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DemoLibrary
{
    public class MemoryCache<Titem>
    {

        private MemoryCache _cache = new MemoryCache(new MemoryCacheOptions());
        private ConcurrentDictionary<object, SemaphoreSlim> _locks = new ConcurrentDictionary<object, SemaphoreSlim>();

        public async Task<Titem> GetOrCreate(object key, Func<Task<Titem>> createItem)
        {
            Titem cacheEntry;
            
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

                         var cacheEntryOptions = new MemoryCacheEntryOptions()
                        .SetSize(1)//Size amount
                                   //Priority on removing when reaching size limit (memory pressure)
                        .SetPriority(CacheItemPriority.High)
                          // Keep in cache for this time, reset time if accessed.
                        .SetSlidingExpiration(TimeSpan.FromSeconds(2))
                        // Remove from cache after this time, regardless of sliding expiration
                        .SetAbsoluteExpiration(TimeSpan.FromSeconds(60));

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
