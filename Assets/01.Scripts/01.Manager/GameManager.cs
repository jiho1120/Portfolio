using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : Singleton<GameManager>
{
    public GameObject pools;
    bool isNew = true; // �����Ѱ� �ҷ�������, ó������
    public bool isWating = false; // ������ȭ���� ����
    public bool isRunTime = true; // ��ư ���� �ٲٱ� ���� ����
    public bool stageStart; // ���� �����°� �������� ��ŸƮ
    public bool gameOver { get; private set; }
    public bool gameClear { get; private set; }
    public GameObject playerPrefab;
    public Player player;
    public Boss boss { get; private set; }

    public int monsterGoal = 0;
    public int killMonster = 0;
    public int countGame { get; private set; } //�̰� ������ ��� ������ gameRound, gameStage
    private int maxStage = 6; // 1����� 5����������
    public int gameRound = 0; //gameRound - gameStage ����
    public int gameStage = 0;
    Coroutine passiveCor = null;
    Coroutine runTimeCor = null; // 5�ʿ��� �پ��
    Coroutine stageTimeCor = null; // 0�ʿ��� �þ



    public float gameTime { get; private set; }
    public float countTime { get; private set; }

    public GameObject bossObj;
    public GameObject canvas;

    public void AllScriptsCorReset()
    {
        CorReset();
        MonsterManager.Instance.CorReset();
        boss.CorReset();
        player.CorReset();
    }
    public void CorReset()
    {
        StopAllCoroutines();
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
        SetCountGame(0);
        for (int i = 1; i < canvas.transform.childCount; i++) // 0�� eventSystem
        {
            canvas.transform.GetChild(i).gameObject.SetActive(false);
        }
        canvas.transform.GetChild(0).gameObject.SetActive(true);
        UiManager.Instance.startScene.SetActive(true);
        //InventoryManager.Instance.itemList.Clear();
    }

    public void LoadMain()
    {
        player.gameObject.SetActive(true);
        boss.gameObject.SetActive(false);
        pools.SetActive(true);
        canvas.SetActive(true);
        if (isNew)
        {
            DoItOnceMain();
            isNew = false;
        }
        player.Init();
        UiManager.Instance.Init();

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
        stageStart = false;
        boss.FirstStart();
        boss.gameObject.SetActive(false);
    }

    public void AddKillMonster(int count = 1)
    {
        killMonster += count;
        UiManager.Instance.SetGameUI();

        if (killMonster >= monsterGoal)
        {
            if (!gameClear)
            {
                GoWatingRoom();
            }
        }
    }

    public void GoWatingRoom() // ���� ī���� �Ǵ� �����÷� ����
    {
        Debug.Log("������ ����");
        ItemManager.Instance.ReturnAllObjectToPool();
        stageStart = false; // �̰� �Ǿ� ���� ����
        gameClear = false; // ������ ��ư ���ؼ� �ʿ���
        gameOver = false; // ������ ��ư ���ؼ� �ʿ���
        countTime = 5f;
        isWating = true;
        MonsterManager.Instance.StopSpawnMonster();
        MonsterManager.Instance.CleanMonster();//=>�� ����ִ� �ֵ鸸 ����. (�ܼ��� ����. 
        if (stageTimeCor != null)
        {
            StopCoroutine(stageTimeCor);
            stageTimeCor = null;
        }
        gameTime = 0;
        killMonster = 0;
        monsterGoal = 0;
        if (passiveCor != null)
        {
            StopCoroutine(passiveCor);
            passiveCor = null;
        }
        UiManager.Instance.wating.SetActive(true);
        UiManager.Instance.SetGameUI(); //�����Ҷ� �ѹ��� ���������
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
            if (isRunTime)
            {
                yield return new WaitForSeconds(1f);
                countTime -= 1;
                UiManager.Instance.SetWaitingUI();
                if (countTime <= 0)
                {
                    stageStart = true;
                    startGame();
                }
            }
            else
            {
                yield return null;
            }

        }
    }
    public void startGame() // ���� �������� ���� ����
    {
        if (runTimeCor != null)
        {
            StopCoroutine(runTimeCor);
            runTimeCor = null;
        }
        UiManager.Instance.wating.SetActive(false);
        isRunTime = false;
        countTime = 5f;
        if (stageTimeCor == null)
        {
            stageTimeCor = StartCoroutine(GameTime());
        }
        SetCountGame(countGame + 1);
        gameRound = (countGame / maxStage) + 1;
        gameStage = countGame % maxStage;
        

        monsterGoal = countGame * 10;
        UiManager.Instance.SetGameUI(); //�����Ҷ� �ѹ��� ���������

        if (passiveCor == null)
        {
            passiveCor = StartCoroutine(CallPassive(true));
        }
        LockCursor(true);

        if (gameStage != 5)
        {
            MonsterManager.Instance.SpawnMonster();
        }
        else
        {
            boss.Init();
            boss.StartWeak();
            monsterGoal = 1;
        }
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
        UiManager.Instance.SetStopTimer();
    }
    public void SkipTime()
    {
        countTime = 0;
        stageStart = true;
        gameTime = 0;
    }

    public void SetCountGame(int num)
    {
        countGame = num;
    }
}