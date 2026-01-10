using BenchmarkDotNet.Attributes;

namespace LRUCacheStrategy;

[MemoryDiagnoser]
public class Benchmarks
{
    private int[] data;
    private LRUCache cache;

    // Test senaryoları: 1000 veri ekle ama kapasite sadece 100 olsun.
    // Bu sayede sürekli silme (Eviction) işlemi tetiklenir.
    [Params(100_000, 1_000_000)]
    public int OperationCount { get; set; }

    [GlobalSetup]
    public void Setup()
    {
        // Random'ı tek bir instance olarak oluştur
        var random = new Random(42); // Sabit seed

        // Veriyi array olarak hazırla (HashSet yerine Array daha hızlı erişilir test için)
        data = new int[OperationCount];
        for (int i = 0; i < OperationCount; i++)
        {
            data[i] = random.Next(0, 5000); // Rastgele sayılar
        }
    }

    [IterationSetup]
    public void PrepareCache()
    {
        // Her ölçümden önce cache'i sıfırla.
        // Kapasiteyi işlem sayısının %10'u yapıyoruz ki cache dolsun ve silme yapsın.
        cache = new LRUCache(OperationCount / 10);
    }

    [Benchmark]
    public void LruCache_PutAndGet()
    {
        for (int i = 0; i < data.Length; i++)
        {
            var val = data[i];
            cache.Put(val, val);
            cache.Get(val);
        }
    }
}