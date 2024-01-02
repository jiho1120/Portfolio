using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public GameObject player;
    
    public Monster[] monsterPrefabs;

    public ObjectPool<Monster> monsterPool { get; private set; }


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        // ObjectPool 인스턴스 생성
        monsterPool = new ObjectPool<Monster>();

        // 몬스터 풀 초기화
        for (int i = 0; i < 2; i++)
        {
            monsterPool.InitializeObjectPool(monsterPrefabs);
        }
        StartCoroutine(GetMonster());

    }
    

    public IEnumerator GetMonster()
    {
        while (true)
        {
            // 두 가지 종류의 몬스터를 랜덤으로 선택하여 소환
            Monster monstersc = monsterPool.GetObjectFromPool(monsterPrefabs);
            monstersc.Init();
            yield return new WaitForSeconds(1f);
        }
    }
}