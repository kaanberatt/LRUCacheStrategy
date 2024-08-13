# LRU (Least Recently Used) Memory Caching Uygulaması

## Genel Bakış

Bu proje, C# dilinde IDistributedCache arayüzünü kullanarak Least Recently Used (LRU) önbelleğini (cache) uygular. Önbellek, kapasitesine ulaştığında en az kullanılan öğeyi silerek verimli bellek kullanımını sağlar. Bu uygulama, hızlı veri erişimi gerektiren durumlar için idealdir.

## Tech Stack

- C#
- .NET 8 Console App
- IDistributedCache
- BenchmarkDotNet

## Algoritma Açıklaması

### LRUCache

`LRUCache`, Least Recently Used (LRU) algoritmasını takip eder. Memory Cache'e yeni bir öğe ekleneceği esnada kapasite dolmuşsa, en az kullanılan öğe çıkarılır ve yeni öğe IDistrubutedCache aracılığıyla ram'e eklenir.

- **Put(int key, int value)**: Key-Value çiftini önbelleğe ekler. Kapasite doluysa, en az kullanılan öğe çıkarılır.
- **Get(int key)**: Key'e karşılık gelen değeri value döner; key yoksa `-1` döner.

## Benchmark Sonuçları

BenchmarkDotNet kullanılarak yapılan testlerde, Memory Cache kullanılarak yapılan LRU algoritmasına ait verileri içermektedir.
100 ve 1000 veri için test yapılmıştır.

![image](https://github.com/user-attachments/assets/09ad188f-e94e-4aeb-ad5b-f90fa15bbd99)

## Kurulum ve Kullanım

1. Bu repoyu klonlayın: `[git clone https://github.com/kaanberatt/LRUCache.git]`
2. Proje dizinine gidin.
3. Projeyi Release modunda derleyin: `dotnet build -c Release`
4. Benchmarkları çalıştırın: `dotnet run -c Release`

## Not : 

Burada amaç en hızlı ve performanslı çözüm değildir. 
MemoryCaching,BenchMark kullanılmak amacıyla yapılmıştır.
