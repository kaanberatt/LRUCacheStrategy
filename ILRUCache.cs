namespace LRUCacheStrategy;

public interface ILRUCache
{
    int Get(int key);

    void Put(int key, int value);
}
