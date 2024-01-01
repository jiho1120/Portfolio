using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public GameObject player;

    public ObjectPool<Monster> monsterPool;

    public Monster[] monsterPrefabs;

    private void Start()
    {
        // ObjectPool �ν��Ͻ� ����
        monsterPool = new ObjectPool<Monster>();

        // ���� Ǯ �ʱ�ȭ
        for (int i = 0; i < 10; i++)
        {
            monsterPool.InitializeObjectPool(monsterPrefabs);
        }
        StartCoroutine(SummonMonster());

    }

    public IEnumerator SummonMonster()
    {
        while (true)
        {
            // �� ���� ������ ���͸� �������� �����Ͽ� ��ȯ
            Monster summonedMonster = monsterPool.GetObjectFromPool(monsterPrefabs);
            if (player.transform != null)
            {
                summonedMonster.SetTarget(player.transform);
            }
            else
            {
                Debug.LogError("Player Transform not assigned!");
            }

            yield return new WaitForSeconds(1f);
        }
    }
}