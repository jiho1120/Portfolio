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
        // ObjectPool 인스턴스 생성
        monsterPool = new ObjectPool<Monster>();

        // 몬스터 풀 초기화
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
            // 두 가지 종류의 몬스터를 랜덤으로 선택하여 소환
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