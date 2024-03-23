using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : Singleton<ObjectPoolManager>
{
    public ObjectPool<Monster> monsterPool;
    public Transform monsterPoolPos;

    Coroutine monCor;

    public void Init()
    {
        monsterPool = new ObjectPool<Monster>(ResourceManager.instance.monsterPre, 10, monsterPoolPos,1);
        monsterPool.Init();
    }
    public void SpawnObject()
    {
        if (monCor == null)
        {
            monCor = StartCoroutine(CorSpawnObject());
        }
    }

    public IEnumerator CorSpawnObject()
    {
        while (true)
        {
            int x = Random.Range(0, 5);
            int z = Random.Range(0, 5);
            monsterPool.SpawnObject(x, z);
            yield return new WaitForSeconds(monsterPool.spawnTime);
        }
    }
    
}
