using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace LRUCacheStrategy;

public class LRUCache : ILRUCache
{
    private readonly IDistributedCache distributedCache;
    private int maxCapacity;
    private int usedCapacity = 0;
    public LRUCache(int capacity)
    {
        if (capacity < 0)
        {
            throw new Exception("The capacity should be an integer.");
        }
        this.maxCapacity = capacity;
        var options = Options.Create(new MemoryDistributedCacheOptions());
        this.distributedCache = new MemoryDistributedCache(options);

    }

    public void Put(int key,int value)
    {
        if (usedCapacity < maxCapacity)
        {
            distributedCache.SetString(key.ToString(), value.ToString());
            Console.WriteLine($"Cache is {key} - {value}");
            usedCapacity++;
            distributedCache.SetString("least", key.ToString());
        }
        else
        {
            int leastKey = GetKeyByLeastRecentlyUsed();
            distributedCache.Remove(leastKey.ToString());
            distributedCache.SetString(key.ToString(), value.ToString());
            Console.WriteLine($"Cache is {key} - {value}");
            distributedCache.SetString("least", value.ToString());
        }
    }

    public int Get(int key) 
    {
        var result = distributedCache.GetString(key.ToString());
        if (result == null)
        {
            Console.WriteLine($"Cache is {-1}");
            return -1;
        }
        else
        {
            distributedCache.SetString("least", key.ToString());
            Console.WriteLine($"Cache is {result}");
            return Convert.ToInt32(result);
        }
    }


    private int GetKeyByLeastRecentlyUsed()
    {
        return Convert.ToInt32(distributedCache.GetString("least"));        
    }
}