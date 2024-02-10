using System.Collections;
using UnityEngine;

public class MonsterManager : Singleton<MonsterManager>, ReInitialize
{
    ObjectPool<Monster> monsterPool = new ObjectPool<Monster>();
    public Transform monsterPoolPos;
    int monsterRange = 10;
    Coroutine monCor = null;
    public void Init()
    {
        MakeMonster();
    }

    public void ReInit()
    {
        throw new System.NotImplementedException();
    }

    public void Deactivate()
    {
        throw new System.NotImplementedException();
    }
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
        if (monsterPool.objectPool.Count == 0)
        {
            // ���� Ǯ �ʱ�ȭ
            for (int i = 0; i < monsterRange; i++)
            {
                monsterPool.RandomInitializeObjectPool(ResourceManager.Instance.monsterAll, monsterPoolPos);
            }
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
            // �� ���� ������ ���͸� �������� �����Ͽ� ��ȯ
            Monster monster = monsterPool.GetObjectFromPool(ResourceManager.Instance.monsterAll, monsterPoolPos);
            if (monster!=null)
            {
                monster.ReInit();
            }            
            yield return new WaitForSeconds(1f);
        }
    }
   
    public void CleanMonster()
    {
        monsterPool.DeleteActive();
    }

    
}
