using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectPooler : MonoBehaviour
{
    

    [System.Serializable] // reveal in inspector
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public Vector3 position;
        public Quaternion rotation;
        public int size;
    }

    #region Singleton
    public static objectPooler Instance;    

    private void Awake()
    {
        Instance = this;
        buildDictionary();
    }
    #endregion

    public List<Pool> _pools;
    /// <summary>
    /// string argument is the "Tag" that identifies the pool.
    /// </summary>
    public Dictionary<string, Queue<GameObject>> _poolDictionary;
    // Start is called before the first frame update
    


    void buildDictionary()
    {
        _poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pool in _pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                PriceScript priceObj = obj.GetComponent<PriceScript>();
                if (i == pool.size - 1)
                {
                    priceObj.LastItem = true;
                }
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            _poolDictionary.Add(pool.tag, objectPool);

        }
        
    }

    Pool findPoolByTag(string tag)
    {
        Pool retPool = null;
        foreach (Pool pool in _pools)
        {
            if (pool.tag == tag)
            {
                retPool = pool;
                break;
            }
        }
        return retPool;
    }

    /// <summary>
    /// Retreive an object from the pool.
    /// </summary>
    /// <param name="tag">The name of the pool</param>
    /// <param name="autoPool">Automatically place retrieved object
    /// back in pool, as it is retreieved.
    /// </param>
    /// <returns></returns>
    public GameObject SpawnFromPool(string tag, bool autoPool = false)
    {
        if (!_poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning($"Pool with tag: {tag} does not exist!");
            return null;
        }
        if (_poolDictionary[tag].Count == 0)
        {
            Debug.LogWarning($"_poolDictionary[{tag}] is empty.");
            return null;
        }

        Pool thisPool = findPoolByTag(tag);

        GameObject objectToSpawn = _poolDictionary[tag].Dequeue();
        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = thisPool.position;
        objectToSpawn.transform.rotation = thisPool.rotation;

        if (autoPool)
        {
            _poolDictionary[tag].Enqueue(objectToSpawn);
        }

        return objectToSpawn;
    }

    public void ReturnToPool(string tag, GameObject poolObject)
    {
        if (!_poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning($"Pool with tag: {tag} does not exist!");
            return;
        }
        poolObject.SetActive(false);
        _poolDictionary[tag].Enqueue(poolObject);

    }


}
