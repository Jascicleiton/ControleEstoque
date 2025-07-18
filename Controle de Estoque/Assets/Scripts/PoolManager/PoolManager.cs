using System;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : Singleton<PoolManager>
{
    [System.Serializable]
    public struct Pool
    {
        public int poolSize;
        public GameObject prefab;
    }

    private Dictionary<int, Queue<GameObject>> poolDicitionary = new Dictionary<int, Queue<GameObject>>();
    [SerializeField] private Pool[] pool = null;
    [SerializeField] private Transform objectPoolTransform = null;

    private void Start()
    {
        // Create object pools on start
        for (int i = 0; i < pool.Length; i++)
        {
            CreatePool(pool[i].prefab, pool[i].poolSize);
        }
    }

    /// <summary>
    /// Ceate a pool of game objects to optimize the instantiation of multiple game objects
    /// </summary>
    private void CreatePool(GameObject prefab, int poolSize)
    {
        int poolKey = prefab.GetInstanceID();
               
        if (!poolDicitionary.ContainsKey(poolKey))
        {
            poolDicitionary.Add(poolKey, new Queue<GameObject>());

            for (int i = 0; i < poolSize; i++)
            {
                GameObject newObject = Instantiate(prefab, objectPoolTransform) as GameObject;
                newObject.SetActive(false);

                poolDicitionary[poolKey].Enqueue(newObject);
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public GameObject ReuseObject(GameObject prefab)
    {
        int poolKey = prefab.GetInstanceID();

        if (poolDicitionary.ContainsKey(poolKey))
        {
            // Get object from pool queue
            GameObject objectToReuse = GetObjectFromPool(poolKey);         

            return objectToReuse;
        }
        else
        {
            Debug.LogWarning("No object pool for " + prefab);
            return null;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    private GameObject GetObjectFromPool(int poolKey)
    {
        // returns the first game object from the queue
        GameObject objectToReuse = poolDicitionary[poolKey].Dequeue();
        // add the object back to the queue in the last position
        poolDicitionary[poolKey].Enqueue(objectToReuse);

        // Log to console if object is currently active
        if (objectToReuse.activeSelf == true)
        {
            objectToReuse.SetActive(false);
        }

        return objectToReuse;
    }
}