namespace LRUCacheStrategy;

public class LRUCache : ILRUCache
{
    // Veriye hızlı erişim için (Key -> LinkedListNode)
    private readonly Dictionary<int, LinkedListNode<CacheItem>> _cacheMap;

    // Sıralamayı tutmak için (Baştaki en güncel, sondaki en az kullanılmış olan)
    private readonly LinkedList<CacheItem> _lruList;

    private readonly int _capacity;

    public LRUCache(int capacity)
    {
        if (capacity <= 0) throw new ArgumentException("Kapasite 0'dan büyük olmalı.");

        _capacity = capacity;
        _cacheMap = new Dictionary<int, LinkedListNode<CacheItem>>(capacity);
        _lruList = new LinkedList<CacheItem>();
    }

    public int Get(int key)
    {
        if (_cacheMap.TryGetValue(key, out var node))
        {
            
            int value = node.Value.Value;

            
            _lruList.Remove(node);
            _lruList.AddFirst(node);

            return value;
        }

        return -1;
    }

    public void Put(int key, int value)
    {
        if (_cacheMap.TryGetValue(key, out var existingNode))
        {
            _lruList.Remove(existingNode);
            existingNode.Value.Value = value;
            _lruList.AddFirst(existingNode);

        }
        else
        {
            if (_cacheMap.Count >= _capacity)
            {
                var lastNode = _lruList.Last;


                _cacheMap.Remove(lastNode.Value.Key);
                _lruList.RemoveLast();

            }

            // Yeni veriyi oluştur ve en başa ekle
            var newItem = new CacheItem { Key = key, Value = value };
            var newNode = new LinkedListNode<CacheItem>(newItem);

            _lruList.AddFirst(newNode);
            _cacheMap.Add(key, newNode);

        }
    }
}

public class CacheItem
{
    public int Key { get; set; }
    public int Value { get; set; }
}