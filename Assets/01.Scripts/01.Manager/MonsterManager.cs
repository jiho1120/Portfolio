using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : Singleton<MonsterManager>
{
    ObjectPool<Monster> monsterPool = new ObjectPool<Monster>();
    public Transform monsterPoolPos;
    int monsterRange = 10;//20; //###############
    Coroutine monCor = null;
    public void CorReset()
    {
        StopAllCoroutines();
        monCor = null;
    }

    public ObjectPool<Monster> MonsterPool()
    {
        return monsterPool;
    }

    public void MakeMonster()
    {
        // 몬스터 풀 초기화
        for (int i = 0; i < monsterRange; i++)
        {
            monsterPool.RandomInitializeObjectPool(ResourceManager.Instance.monsterAll, monsterPoolPos);
        }
    }

    public void SpawnMonster()
    {
        if (monCor == null)
        {
            monCor = StartCoroutine(GetMonster());
        }
    }
    public void StopSpawnMonster()
    {
        if (monCor != null)
        {
            StopCoroutine(monCor);
            monCor = null;
        }
    }

    public IEnumerator GetMonster()
    {
        while (true)
        {
            // 두 가지 종류의 몬스터를 랜덤으로 선택하여 소환
            Monster monster = monsterPool.GetObjectFromPool(ResourceManager.Instance.monsterAll, monsterPoolPos);
            if (monster!=null)
            {
                monster.Init();
            }            
            yield return new WaitForSeconds(1f);
        }
    }
   
   
    public void CleanMonster()
    {
        monsterPool.DeleteActive();
    }
}
