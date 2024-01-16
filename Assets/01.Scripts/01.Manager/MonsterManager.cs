using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : Singleton<MonsterManager>
{
    ObjectPool<Monster> monsterPool = new ObjectPool<Monster>();
    public Transform monsterPoolPos;
    int monsterRange = 20;
    Coroutine monCor = null;

    public void Init()
    {
        MakeMonster();
    }
    public ObjectPool<Monster> MonsterPool()
    {
        return monsterPool;
    }

    void MakeMonster()
    {
        // ���� Ǯ �ʱ�ȭ
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

    public IEnumerator GetMonster()
    {
        while (true)
        {
            // �� ���� ������ ���͸� �������� �����Ͽ� ��ȯ
            Monster monstersc = monsterPool.GetObjectFromPool(ResourceManager.Instance.monsterAll, monsterPoolPos);
            monstersc.Init();
            yield return new WaitForSeconds(1f);
        }
    }
   
}
