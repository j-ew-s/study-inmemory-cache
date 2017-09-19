# study-inmemory-cache

Caching is a cool approache to use in order to improve our app's performance reducing work to generate a content avoiding calling again and again methods, layers and DB.

When you cache data, the app will create a copy in memory and this is why it will return faster than first time.

Cache is a good approache to use when you have data that does not change every time; like User Address.

For this study I am using:
+ Dot Net Core;
+ IMemoryCache;
+ API;

##### IMemoryCache :
It represents the Cache Stored in the Server's memory.

#### Setting up

We will start by ```Startup.cs``` file setting the ```ConfigureServices``` method adding the ```services.AddMemoryCache()```.
In memory cache is a service and will reference it using **D.I**.

```cs
services.AddMemoryCache();
```

Now, on Controller  we should set the IMemoryCache instance to our controller's constructor.

```cs
 private readonly IMemoryCache _cache;

        public CacheController(IMemoryCache memoryCache)
        {
            _cache = memoryCache;
        }
```

Now we can access ```IMemoyCache``` and we can use ```_cache``` to set or get our cached data.

On this example, I will create two methods, SetCache and GetCache.

##### SetCache Method

```cs
            var time = DateTime.Now.ToString();
            var cacheOptions = new MemoryCacheEntryOptions()
            {
                AbsoluteExpirationRelativeToNow = (DateTime.Now.AddMinutes(1) - DateTime.Now)
            };
            _cache.Set("CachedTime", time, cacheOptions);
            return time + " Is now cached.";
```

At first I just created a variable ```time``` that will store the current time.
Then I used the ```MemoryCacheEntryOptions``` to set a expiration time. So our cached data will be stored for 1 minute.

Then I set a new ```_cache``` with the key "CachedTime" with the Current time and expiration time. 
Ever time I whant the time stored on ```time``` variable, I should look into ```_cache``` using this "CachedTime" key.

##### GetCache Method

```cs
            var time = string.Empty;
            if (!_cache.TryGetValue("CachedTime", out time))
            {
                time = "Time is not cached.";
            }
            return time;
```

I set a new variale time to store the current value on ```_cache``` under "CachedTime" key.
To verify if the ```_cache``` have any value set to "CachedTime" key, I am using this TryGetValye. 
If there is no Key or the Key has its value set to null, the method will return false, otherwise true and will set to time variable the value set to this key.

##### Testing.

Running the application, go to http://localhost/api/values/GetCache 
As it is the first time, you must see the "Time is not cached." message. 

Go to http://localhost/api/values/SetCache 
The message returned will show the currente date time. And if you go back to http://localhost/api/values/GetCache 
you will see the cached date.

After one minute, access the GetCache method again and the "Time is not cached." message should be presented due the expiration time.
