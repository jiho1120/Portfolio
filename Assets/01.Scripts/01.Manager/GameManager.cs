using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ������
// �� ���� �ȷ� ����

//���� ����
// �߷� ȿ�� ��ġ��
// �׶��� ���� ��ġ��
//��ƮŸ�̹�
//���� 
//��ų
//�׺���̼� ��ž���� ��ġ��(����)

public class GameManager : Singleton<GameManager>
{
    public Player player;
    public bool gameStart;
    public bool isRunTime = true; // ��ư ���� �ٲٱ� ���� ����
    public int monsterGoal = 0;
    public int killMonster = 0;
    private int countGame = 4; //�̰� ������ ��� ������ gameRound, gameStage
    private int maxStage = 6; // 1����� 5����������
    public int gameRound = 0; //gameRound - gameStage ����
    public int gameStage = 0;
    Coroutine startGameCor = null;
    public Coroutine goWatingRoom = null;
    Coroutine passiveCor = null;
    Coroutine runTimeCor = null;


    public float gameTime { get; private set; }
    public float countTime { get; private set; }

    public Boss boss { get; private set; }
    public GameObject bossObj;


    private void Start()
    {
        DoItOnceMain();
    }
    public void DoItOnceMain()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        ResourceManager.Instance.LoadResources();
        player.FirstStart();
        SkillManager.Instance.Init();
        MonsterManager.Instance.Init();
        InventoryManager.Instance.Init();
        ItemManager.Instance.Init();
        player.CalcPlayerStat();
        UiManager.Instance.Init();
        countTime = 5f;
        UiManager.instance.wating.SetActive(true);
        gameStart = false;
        runTimeCor = StartCoroutine(RunTime());
        boss = Instantiate(bossObj).GetComponent<Boss>();
        boss.FirstStart();
        boss.gameObject.SetActive(false);
    }


    private void Update()
    {
        UiManager.Instance.SetUI();
        if (killMonster != 0 && killMonster != 0)
        {
            if (killMonster >= monsterGoal && gameStart == true)
            {
                gameStart = false;
                if (goWatingRoom == null)
                {
                    goWatingRoom = StartCoroutine(GoWatingRoom());
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            killMonster += 10;
        }
        
    }

    IEnumerator RunTime()
    {
        while (true)
        {
            if (isRunTime)
            {
                yield return new WaitForSeconds(1f);
                countTime -= 1;
                if (countTime <= 0)
                {
                    gameStart = true;
                    if (startGameCor == null)
                    {
                        startGameCor = StartCoroutine(startGame());
                    }
                }
            }
            else
            {
                yield return null;
            }

        }
    }
    IEnumerator GameTime()
    {
        while (gameStart)
        {
            gameTime += 1;
            yield return new WaitForSeconds(1f);
        }
    }
    IEnumerator startGame()
    {
        if (runTimeCor != null)
        {
            StopCoroutine(runTimeCor);
            runTimeCor = null;
        }
        UiManager.instance.wating.SetActive(false);
        if (goWatingRoom != null)
        {
            StopCoroutine(goWatingRoom);
            LockCursor(false);
            goWatingRoom = null;
        }
        StartCoroutine(GameTime());
        isRunTime = false;
        countTime = 5f;
        gameRound = (countGame / maxStage) + 1;
        gameStage = (countGame % maxStage) + 1;
        monsterGoal = (countGame + 1) * 10;

        if (passiveCor == null)
        {
            passiveCor = StartCoroutine(CallPassive(true));
        }
        LockCursor(true);

        if (gameStage != 5)
        {
            if (gameRound != 1 && gameStage != 1)
            {
                MonsterManager.Instance.monstersLevelUp();
            }
            MonsterManager.Instance.SpawnMonster();
        }
        else
        {
            boss.Init();
            boss.StartWeak();
            monsterGoal = 1;
        }
        yield return null;
    }
    IEnumerator GoWatingRoom()
    {
        MonsterManager.Instance.StopSpawnMonster();
        MonsterManager.Instance.CleanMonster();//=>�� ����ִ� �ֵ鸸 ����. (�ܼ��� ����. 
        StopCoroutine(GameTime());
        if (startGameCor != null)
        {
            StopCoroutine(startGameCor);
            startGameCor = null;
        }
        killMonster = 0;
        monsterGoal = 0;
        gameTime = 0;
        countGame++;
        isRunTime = true;
        if (passiveCor != null)
        {
            StopCoroutine(passiveCor);
            passiveCor = null;
        }
        UiManager.instance.wating.SetActive(true);
        if (runTimeCor == null)
        {
            runTimeCor = StartCoroutine(RunTime());
        }
        LockCursor(false);

        yield return null;
    }

    public void LockCursor(bool cursorLock)
    {
        if (cursorLock)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public IEnumerator CallPassive(bool isPlayer)
    {
        while (gameStart)
        {
            CallPassiveSkill(isPlayer);
            yield return new WaitForSeconds(10f);
        }
    }
    public void CallPassiveSkill(bool isPlayer)
    {
        if (SkillManager.Instance.passiveSkill != null)
        {
            SkillManager.Instance.passiveSkill.DoReset();

        }
        SkillManager.Instance.CallPassiveSkill(isPlayer);
    }
    public void StopOnOffTime()
    {
        isRunTime = !isRunTime;
    }
    public void SkipTime()
    {
        countTime = 0;
        gameStart = true;
        gameTime = 0;
    }
}