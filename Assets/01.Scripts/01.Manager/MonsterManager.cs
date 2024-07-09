using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using static AllEnum;

public class MonsterManager : Singleton<MonsterManager>
{
    [SerializeField] MonsterFactory factory;

    // stack-based ObjectPool
    [SerializeField] private IObjectPool<Monster> objectPool;

    // 이미 풀에 있는 기존 항목을 반환하려고 하면 예외를 던집니다.
    [SerializeField] private bool collectionCheck = true;
    // 풀 용량 및 최대 크기를 제어하는 추가 옵션
    [SerializeField] private int defaultCapacity = 20;
    [SerializeField] private int maxSize = 100;

    [SerializeField] private Vector3 spawnArea = new Vector3(10, 0, 10);

    private float nextTimeToCreate = 1f;
    // deactivate after delay
    public float timeoutDelay { get; private set; }

    Coroutine monSpawnCor = null;
    
    private void Start()
    {
        if (objectPool == null)
        {
            objectPool = new ObjectPool<Monster>(CreateMonster,
               OnGetFromPool, OnReleaseToPool, OnDestroyPooledObject,
               collectionCheck, defaultCapacity, maxSize);
        }
    }
    public void Init()
    {
        LevelUp();
        if (monSpawnCor == null)
        {
            monSpawnCor = StartCoroutine(SpawnMonster());
        }

        // 클리어 할때 0초로 만든거 다시 2초로 바꿈
        timeoutDelay = 2f;
    }
    public void ClearObjectPool()
    {
        objectPool.Clear();
    }
    private Monster CreateMonster()
    {
        Monster monster;
        if (Random.value < 0.5)
        {
            monster = (Monster)factory.GetProduct(MonsterType.NormalMonster.ToString());
        }
        else
        {
            monster = (Monster)factory.GetProduct(MonsterType.ExplosionMonster.ToString());
        }
        monster.ObjectPool = objectPool;
        return monster;
    }

    // 몬스터 반환 메서드
    private void OnReleaseToPool(Monster pooledObject)
    {
        pooledObject.Deactivate();
    }

    private void OnGetFromPool(Monster pooledObject)
    {
        if (pooledObject == null)
        {
            Debug.LogWarning("pooledObject is null");
            return;
        }
        SetEnemyPos(pooledObject);
        pooledObject.Activate();
    }

    private void OnDestroyPooledObject(Monster pooledObject)
    {
        Destroy(pooledObject.gameObject);
    }

    public Monster GetMonster()
    {
        Monster monster = objectPool.Get();
        if (monster != null)
        {
            SetEnemyPos(monster);
        }
        return monster;
        
    }

    IEnumerator SpawnMonster()
    {
        while (GameManager.Instance.stageStart)
        {
            Monster monster = GetMonster();
            if (monster != null)
            {
                SetEnemyPos(monster);
            }
            yield return new WaitForSeconds(nextTimeToCreate);
        }
        monSpawnCor = null;
        
    }

    public void SetEnemyPos(Creature monster)
    {
        Vector3 playerPos = GameManager.Instance.player.transform.position;
        // 랜덤 위치 계산
        Vector3 randomPosition = new Vector3(
            Random.Range(-spawnArea.x, spawnArea.x),
            0,
            Random.Range(-spawnArea.z, spawnArea.z)
        );

        monster.transform.position = randomPosition + playerPos;
    }


    public void SetTimeDelay(float time)
    {
        timeoutDelay = time;
    }

    public void LevelUp()
    {
        StatUp();
    }
    public void StatUp()
    {
        DataManager.Instance.gameData.monsterData.monsterStat.StatUp(1, 20, 20, 2, 1, 0.5f, 0.1f, 5, 100, 0, 0, 0, 0, 3, 0);

    }
}
