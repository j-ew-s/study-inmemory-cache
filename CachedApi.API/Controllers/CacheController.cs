using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace CachedApi.API.Controllers
{
    public class CacheController : Controller
    {
        private readonly IMemoryCache _cache;

        public CacheController(IMemoryCache memoryCache)
        {
            _cache = memoryCache;
        }
        // GET api/values
        [HttpGet("SetCache")]
        public string SetCache()
        {
            var time = DateTime.Now.ToString();
            var cacheOptions = new MemoryCacheEntryOptions()
            {
                AbsoluteExpirationRelativeToNow = (DateTime.Now.AddMinutes(1) - DateTime.Now)
            };
            _cache.Set("CachedTime", time, cacheOptions);
            return time + " Is now cached.";
        }
        [HttpGet("GetCache")]
        public string GetCache()
        {
            var time = string.Empty;
            if (!_cache.TryGetValue("CachedTime", out time))
            {
                time = "Time is not cached.";
            }
            return time;
        }
    }
}
