using BenchmarkDotNet.Running;
using LRUCacheStrategy;
class Program
{
    static void Main(string[] args)
    {
        LRUCache cache = new LRUCache(2);

        cache.Put(1, 1); // Cache is {1=1}
        cache.Put(2, 2); // Cache is {1=1, 2=2}
        cache.Get(1);    // returns 1, Cache is {2=2, 1=1}
        cache.Put(3, 3); // LRU key 2 is evicted, Cache is {1=1, 3=3}
        cache.Get(2);    // returns -1 (not found)
        cache.Put(4, 4); // LRU key 1 is evicted, Cache is {3=3, 4=4}
        cache.Get(1);    // returns -1 (not found)
        cache.Get(3);    // returns 3
        cache.Get(4);    // returns 4

        #region Benchmark
        // Not : release modda çalıştırılmalıdır.
        var summary = BenchmarkRunner.Run<Benchmarks>();

        #endregion


        Console.ReadLine();
    }
}