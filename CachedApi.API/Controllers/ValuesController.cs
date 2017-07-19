using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace CachedApi.API.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private readonly IMemoryCache _cache;

        public ValuesController(IMemoryCache memoryCache)
        {
            _cache = memoryCache;
        }
        // GET api/values
        [HttpGet]
        public string Get()
        {
            string cacheKey = "UpdatedTime";
            DateTime cacheEntry;

            if (!_cache.TryGetValue(cacheKey, out cacheEntry))
            {
                cacheEntry = DateTime.Now;

                var cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(3.00));

                _cache.Set(cacheKey, cacheEntry, cacheEntryOptions);
            }

            return cacheEntry.ToString();
        }
    }
}
