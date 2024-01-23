using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ������
// �� ���� �ȷ� ����
// �߷� ȿ�� ��ġ��
// �׶��� ���� ��ġ��
//���� ����
//���� 
//�׺���̼� ��ž���� ��ġ��(����)

public class GameManager : Singleton<GameManager>
{
    public GameObject pools;
    bool isNew = true; // �����Ѱ� �ҷ������� ó������
    public bool isWating = false; // ������ȭ���� ����
    public bool isRunTime = true; // ��ư ���� �ٲٱ� ���� ����
    public bool stageStart; // ���� �����°� ���� ��ŸƮ
    public bool gameOver { get; private set; }
    public bool gameClear { get; private set; }
    public GameObject playerPrefab;
    public Player player;
    public Boss boss { get; private set; }

    public int monsterGoal = 0;
    public int killMonster = 0;
    private int countGame = 4; //�̰� ������ ��� ������ gameRound, gameStage
    private int maxStage = 6; // 1����� 5����������
    public int gameRound = 0; //gameRound - gameStage ����
    public int gameStage = 0;
    Coroutine startGameCor = null;
    public Coroutine goWatingRoom = null;
    Coroutine passiveCor = null;
    Coroutine runTimeCor = null; // 5�ʿ��� �پ��


    public float gameTime { get; private set; }
    public float countTime { get; private set; }

    public GameObject bossObj;
    public GameObject canvas;

    public void AllScriptsCorReset()
    {
        CorReset();
        MonsterManager.Instance.CorReset();
        boss.CorReset();
        //MonsterManager.Instance.CorReset();
    }
    public void CorReset()
    {
        StopAllCoroutines();        
        startGameCor = null;
        passiveCor = null;
        runTimeCor = null;
    }
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        boss = GameObject.FindGameObjectWithTag("Boss").GetComponent<Boss>();
        SceneLoadController.Instance.GoStartScene();
    }

    private void Update()
    {        

        if (Input.GetKeyDown(KeyCode.KeypadPlus))
        {
            //killMonster += 10;            
            AddKillMonster(10);
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            player.SetHp(0);
        }

    }
    public void LoadStartScene()
    {
        player.gameObject.SetActive(false);
        
        pools.SetActive(false);
        canvas.SetActive(true);
        monsterGoal = 0;
        killMonster = 0;
        countGame = 0;
        for (int i = 1; i < canvas.transform.childCount; i++) // 0�� eventSystem
        {
            canvas.transform.GetChild(i).gameObject.SetActive(false);
        }
        canvas.transform.GetChild(0).gameObject.SetActive(true);
        UiManager.Instance.startScene.SetActive(true);       
    }

    public void LoadMain()
    {
        player.gameObject.SetActive(true);
        player.Init();
        boss.gameObject.SetActive(false);
        pools.SetActive(true);
        canvas.SetActive(true);
        if (isNew)
        {
            DoItOnceMain();
            isNew = false;
        }
        gameClear = false;
        gameOver = false;
        isWating = true;
        
        countTime = 5f;
        
        UiManager.instance.Init();
        InventoryManager.Instance.Init();

        GoWatingRoom();
    }

    public void DoItOnceMain()
    {
        ResourceManager.Instance.LoadResources();
        player.FirstStart();
        SkillManager.Instance.Init();
        MonsterManager.Instance.MakeMonster(); // ������Ʈ Ǯ�� ����
        ItemManager.Instance.Init(); // ����ɾ����� ������ƮǮ����
        InventoryManager.Instance.Init(); // �κ��丮 ������ ĭ ����
        player.CalcPlayerStat();
        UiManager.Instance.FirstSet();
        countTime = 5f;
        stageStart = false;
        boss.FirstStart();
        boss.gameObject.SetActive(false);
    }

    public void AddKillMonster(int count =1)
    {
        killMonster+= count;
        UiManager.instance.SetGameUI();

        if (killMonster >= monsterGoal )
        {
            GoWatingRoom();
        }
    }

    void GoWatingRoom()
    {
        Debug.Log("������ �ڷ�ƾ ����");
        isWating = true;
        stageStart = false;
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
        if (passiveCor != null)
        {
            StopCoroutine(passiveCor);
            passiveCor = null;
        }
        UiManager.instance.wating.SetActive(true);
        isRunTime = true;
        if (runTimeCor == null)
        {
            runTimeCor = StartCoroutine(RunTime());
        }
        LockCursor(false);
    }
    IEnumerator RunTime()
    {
        while (true)
        {
            Debug.Log("isRunTime : "+ isRunTime);
            if (isRunTime)
            {
                yield return new WaitForSeconds(1f);
                countTime -= 1;
                UiManager.Instance.SetWatingUI();
                if (countTime <= 0)
                {
                    stageStart = true;
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
        countGame++;
        gameRound = (countGame / maxStage) + 1;
        gameStage = countGame % maxStage;
        monsterGoal = countGame * 10;

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
    IEnumerator GameTime()
    {
        while (stageStart)
        {
            gameTime += 1;
            yield return new WaitForSeconds(1f);
        }
    }
    public void SetGameOver()
    {
        gameOver = true;
    }
    public void SetGameClear(bool clear = true)
    {
        gameClear = clear;
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
        while (stageStart)
        {
            PassiveSkill ps = CallPassiveSkill(isPlayer);
            yield return new WaitForSeconds(ps.skillStat.duration);
        }
    }
    public PassiveSkill CallPassiveSkill(bool isPlayer)
    {
        if (SkillManager.Instance.passiveSkill != null)
        {
            SkillManager.Instance.passiveSkill.DoReset();

        }
        PassiveSkill ps = SkillManager.Instance.CallPassiveSkill(isPlayer);
        return ps;
    }
    public void StopOnOffTime()
    {
        isRunTime = !isRunTime;
        UiManager.Instance.StopTimer();
    }
    public void SkipTime()
    {
        countTime = 0;
        stageStart = true;
        gameTime = 0;
    }
}