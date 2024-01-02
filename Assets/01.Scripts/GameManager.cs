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
        // ObjectPool �ν��Ͻ� ����
        monsterPool = new ObjectPool<Monster>();

        // ���� Ǯ �ʱ�ȭ
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
            // �� ���� ������ ���͸� �������� �����Ͽ� ��ȯ
            Monster monstersc = monsterPool.GetObjectFromPool(monsterPrefabs);
            monstersc.Init();
            yield return new WaitForSeconds(1f);
        }
    }
}