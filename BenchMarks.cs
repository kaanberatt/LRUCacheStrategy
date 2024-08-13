using BenchmarkDotNet.Attributes;

namespace LRUCacheStrategy;

[MemoryDiagnoser]
// Bellek kullanımını ölçer
public class Benchmarks
{
    private HashSet<int> randomValues = new();

    [Params(1_00,1_000)]
    public int RandomValueCount { get; set; }

    [IterationSetup]
    public void IterationSetup()
    {
        randomValues = Enumerable.Range(0, RandomValueCount).Select(_ => new Random().Next(0, RandomValueCount)).ToHashSet();
    }

    [Benchmark]
    public void LruCacheSolution()
    {
        var cache = new LRUCache(RandomValueCount);

        foreach (var value in randomValues)
        {
            cache.Put(value, value);
            cache.Get(value);
        }
    }
}