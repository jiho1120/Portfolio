using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : Singleton<MonsterManager>
{
    ObjectPool<Monster> monsterPool = new ObjectPool<Monster>();
    int monsterRange = 20;


    void Start()
    {
        
    }
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
        for (int i = 0; i < 1; i++)
        {
            monsterPool.RandomInitializeObjectPool(ResourceManager.Instance.monsterAll);
        }
    }

    public void SpawnMonster()
    {
        StartCoroutine(GetMonster());
    }

    public IEnumerator GetMonster()
    {
        while (true)
        {
            // �� ���� ������ ���͸� �������� �����Ͽ� ��ȯ
            Monster monstersc = monsterPool.GetObjectFromPool(ResourceManager.Instance.monsterAll);
            monstersc.Init();
            yield return new WaitForSeconds(1f);
        }
    }
   
}
