using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> where T : MonoBehaviour
{
    private Queue<T> pool = new Queue<T>();
    private Transform tr;
    private List<T> prefabList = new List<T>();
    int initialSize;
    public float spawnTime { get; private set; }

    public ObjectPool(T[] prefab, int _initialSize, Transform _tr, float _spawnTime)
    {
        for (int i = 0; i < prefab.Length; i++)
        {
            prefabList.Add(prefab[i]);
        }
        initialSize = _initialSize;
        tr = _tr;
        spawnTime = _spawnTime;

    }
    public void Init()
    {
        int num = prefabList.Count;
        for (int i = 0; i < initialSize; i++)
        {
            int ranNum = Random.Range(0, num);
            GeneratePool(prefabList[ranNum]);
        }
    }
    public void GeneratePool(T prefab)
    {
        T obj = Object.Instantiate(prefab, tr).GetComponent<T>();
        obj.gameObject.SetActive(false);
        pool.Enqueue(obj);
    }

    public void SpawnObject(int x, int z)
    {
        T obj = GetObjectFromPool();
        obj.gameObject.SetActive(true);
        SetPosition(obj, x, z);
        ObjInitialize(obj);
    }

    public void SetPosition(T obj, int x, int z)
    {
        Vector3 spawnPos = new Vector3(x, 0, z);
        obj.transform.position = spawnPos;
        obj.transform.rotation = Quaternion.identity;
    }


    public T GetObjectFromPool()
    {
        T obj;
        if (pool.Count == 0)
        {
            int num = prefabList.Count;
            int ranNum = Random.Range(0, num);
            obj = Object.Instantiate(prefabList[ranNum], tr).GetComponent<T>();
        }
        else
        {
            obj = pool.Dequeue();
            obj.gameObject.SetActive(true);
        }
        return obj;
    }

    public void ReturnObjectToPool(T obj)
    {
        obj.gameObject.SetActive(false);
        pool.Enqueue(obj);
    }
    public void ObjInitialize(T tInfo)
    {
        if (tInfo.GetComponent<Initialize>() == null)
        {
            Debug.Log("Initialize ¾øÀ½");
        }
        else
        {
            Initialize Initialize;
            Initialize = tInfo.GetComponent<Initialize>();
            if (Initialize != null)
            {
                Initialize.Init();
            }
        }
        
        
        
    }
}
