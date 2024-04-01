using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : Singleton<MonsterManager>
{
    public ObjectPool<Monster> monsterPool;
    public Transform monsterPoolPos;

    Coroutine monCor = null;
    int monsterCount = 1;
    float monsterTime = 10;

    public void Init()
    {
        if (monsterPool ==null)
        {
            monsterPool = new ObjectPool<Monster>(ResourceManager.instance.monsterPre, monsterCount, monsterPoolPos, monsterTime);
            monsterPool.Init();
        }
        SpawnObject();
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
    public void ReturnToPool(Monster mon)
    {
        monsterPool.ReturnObjectToPool(mon);
    }

}
