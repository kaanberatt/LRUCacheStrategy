# LRU Cache (C#)

## Genel Bakış
Bu proje, C# ve .NET 8 kullanılarak geliştirilmiş, **O(1)** zaman karmaşıklığına sahip yüksek performanslı bir **LRU (Least Recently Used)** önbellek implementasyonudur. `Dictionary` ve `Doubly Linked List` hibrit yapısı kullanılarak, bellek tahsisi (allocation) minimize edilmiş ve erişim süreleri **nanosaniye** seviyesine indirilmiştir.

## Tech Stack
- **Dil:** C#
- **Veri Yapıları:** `Dictionary<K, Node>`, `LinkedList<T>`
- **Test:** BenchmarkDotNet

## Algoritma ve Performans
Bu implementasyon, LRU mantığını en verimli şekilde işletmek için iki veri yapısını senkronize kullanır:
* **Dictionary:** Veriye $O(1)$ sürede erişim sağlar.
* **Doubly Linked List:** Verilerin kullanım sırasını tutar. En son erişilen veri başa (head) taşınır, kapasite dolduğunda sondaki (tail) veri silinir.

### Metotlar
- **Put(key, value):** Veriyi ekler veya günceller. Kapasite doluysa `Eviction` (silme) mekanizması devreye girer. Karmaşıklık: $\mathcal{O}(1)$
- **Get(key):** Veriyi getirir ve kullanım sırasını günceller (Most Recently Used). Karmaşıklık: $\mathcal{O}(1)$

## Benchmark Sonuçları
BenchmarkDotNet ile yapılan stres testlerinde (1 Milyon İşlem) için aşağıdaki sonuçlar elde edilmiştir:

| Operation Count | Mean Time | Time per Op | Gen 0 | Allocated |
|----------------:|----------:|------------:|------:|----------:|
| **100,000** | 6.00 ms   | **60 ns** | -     | 351 KB    |
| **1,000,000** | 34.10 ms  | **34 ns** | -     | 351 KB    |

> **Not:** Veri seti 10 katına çıktığında bile işlem başına süre (34ns) sabit kalmaktadır. Bu, algoritmanın $O(1)$ çalıştığının kanıtıdır.
