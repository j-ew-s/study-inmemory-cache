﻿using System;
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
