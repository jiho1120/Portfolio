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

    // �̹� Ǯ�� �ִ� ���� �׸��� ��ȯ�Ϸ��� �ϸ� ���ܸ� �����ϴ�.
    [SerializeField] private bool collectionCheck = true;
    // Ǯ �뷮 �� �ִ� ũ�⸦ �����ϴ� �߰� �ɼ�
    [SerializeField] private int defaultCapacity = 20;
    [SerializeField] private int maxSize = 100;

    [SerializeField] private Vector3 spawnArea = new Vector3(10, 0, 10);

    private float nextTimeToCreate = 1f;
    // deactivate after delay
    public float timeoutDelay { get; private set; }

    Coroutine monSpawnCor = null;
    protected override void Awake()
    {
        base.Awake();
        objectPool = new ObjectPool<Monster>(CreateMonster,
               OnGetFromPool, OnReleaseToPool, OnDestroyPooledObject,
               collectionCheck, defaultCapacity, maxSize);
    }

    public void Init()
    {
        LevelUp();
        monSpawnCor = StartCoroutine(SpawnMonster());

        // Ŭ���� �Ҷ� 0�ʷ� ����� �ٽ� 2�ʷ� �ٲ�
        timeoutDelay = 2f;
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

    // ���� ��ȯ �޼���
    private void OnReleaseToPool(Monster pooledObject)
    {
        pooledObject.Deactivate();


    }

    private void OnGetFromPool(Monster pooledObject)
    {
        SetEnemyPos(pooledObject);
        pooledObject.Activate();
    }

    // invoked when we exceed the maximum number of pooled items (i.e. destroy the pooled object)
    private void OnDestroyPooledObject(Monster pooledObject)
    {
        Destroy(pooledObject.gameObject);
    }

    public Monster GetMonster()
    {
        Monster monster = objectPool.Get();
        SetEnemyPos(monster);

        return monster;
    }

    IEnumerator SpawnMonster()
    {
        while (GameManager.Instance.stageStart)
        {
            GetMonster();
            yield return new WaitForSeconds(nextTimeToCreate);
        }
        StopCoroutine(monSpawnCor);
        monSpawnCor = null;
    }

    public void SetEnemyPos(Creature monster)
    {
        Vector3 playerPos = GameManager.Instance.player.transform.position;
        // ���� ��ġ ���
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
