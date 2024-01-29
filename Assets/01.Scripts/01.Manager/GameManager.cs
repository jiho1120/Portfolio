using System.Collections;
using UnityEngine;
//�˹� ,������ ������°� ���� ��ġ��
// �Ŀ��� �ǳ� �ٽý��۽� ������ 

public class GameManager : Singleton<GameManager>
{
    public GameObject pools;
    public GameObject playerPrefab;
    public Player player { get; private set; }
    public Boss boss { get; private set; }
    public bool isNew { get; private set; } // �����Ѱ� �ҷ�������, ó������
    public bool isRunTime { get; private set; } // ��ư ���� �ٲٱ� ���� ����
    public bool stageStart { get; private set; } // ���� �����°� �������� ��ŸƮ
    public bool gameOver { get; private set; }
    public bool gameClear { get; private set; }

    public int monsterGoal { get; private set; }
    public int killMonster { get; private set; }
    public int countGame { get; private set; } //�̰� ������ ��� ������ gameRound, gameStage , 1���� ����
    private int maxStage = 5; // 1����� 5����������
    public int gameRound { get; private set; } //gameRound - gameStage ����
    public int gameStage { get; private set; }
    Coroutine runTimeCor = null; // 5�ʿ��� �پ��
    Coroutine stageTimeCor = null; // 0�ʿ��� �þ
    public AudioSource audioSource { get; private set; }

    public float gameTime { get; private set; }
    public float countTime { get; private set; }

    public GameObject bossObj;
    public int StopNum; // ���� ������ Ŀ���� �ٸ� â�� ���ؼ� Ǯ�������� �� ����, 0�ϋ��� �۵��ǰ�

    #region �ʱ�ȭ ����
    public void SetUpOnce() // ó�� �����Ұ�
    {
        audioSource = GetComponent<AudioSource>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        boss = GameObject.FindGameObjectWithTag("Boss").GetComponent<Boss>();
        SceneLoadController.Instance.GoStartScene();

    }
    public void LoadStartScene()
    {
        //audioSource.Play();
        Time.timeScale = 1f;
        player.gameObject.SetActive(false);
        UiManager.Instance.canvas.SetActive(true);
        pools.SetActive(false);
        SetIsNew(true);
        SetIsRunTime(false);
        SetStageStart(false);
        SetGameOver(false);
        SetGameClear(false);
        StopNum = 0;
        monsterGoal = 0;
        SetKillMonster(0);
        SetCountGame(1);
        player.PassiveCorReset();

        for (int i = 1; i < UiManager.Instance.canvas.transform.childCount; i++) // 0�� eventSystem
        {
            UiManager.Instance.canvas.transform.GetChild(i).gameObject.SetActive(false);
        }
        UiManager.Instance.canvas.transform.GetChild(0).gameObject.SetActive(true);
        UiManager.Instance.startScene.SetActive(true);
    }
    public void Recycle() // �����Ҷ� 
    {
        //audioSource.Play();

    }
    public void Deactivate() //��Ȱ��ȭ �Ҷ�
    {

    }
    public void DontUse() // �Ⱦ��� �ɶ�
    {

    }
    #endregion

    #region Set�Լ�
    public void SetIsNew(bool _bool)
    {
        isNew = _bool;
        
    }
    
    public void SetIsRunTime(bool _bool)
    {
        isRunTime = _bool;
    }
    public void SetStageStart(bool _bool)
    {
        stageStart = _bool;
    }
    public void SetGameOver(bool _bool = true)
    {
        gameOver = _bool;
    }
    public void SetGameClear(bool clear = true)
    {
        gameClear = clear;
    }
    public void SetCountGame(int num)
    {
        countGame = num;
    }
    public void SetKillMonster(int num)
    {
        killMonster = num;
    }
    public void SetGameStage(int num)
    {
        gameStage = num;
    }
    public void SetGameRound(int num)
    {
        gameRound = num;
    }

    #endregion
    public void AllScriptsCorReset()
    {
        CorReset();
        MonsterManager.Instance.CorReset();
        boss.CorReset();
        player.AllCorReset();
    }

    public void CorReset()
    {
        StopAllCoroutines();
        
        if (runTimeCor != null)
        {
            runTimeCor = null;
        }
    }

    private void Start()
    {
        SetUpOnce();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        if (Input.GetKeyDown(KeyCode.KeypadPlus))
        {
            AddKillMonster(10);
        }
    }
    

    public void LoadMain()
    {
        player.gameObject.SetActive(true);
        boss.gameObject.SetActive(false);
        pools.SetActive(true);
        UiManager.Instance.canvas.SetActive(true);
        if (isNew)
        {
            DoItOnceMain();
            SetIsNew(false);
        }
        player.Init();
        UiManager.Instance.Init();
        UiManager.Instance.playerConditionUI.SetUI();

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

    

    public void GoWatingRoom() // ���� ī���� �Ǵ� �����÷� ����
    {
        ItemManager.Instance.ReturnAllObjectToPool();
        MonsterManager.Instance.StopSpawnMonster();
        MonsterManager.Instance.CleanMonster();//=>�� ����ִ� �ֵ鸸 ����. (�ܼ��� ����. 
        stageStart = false; // �̰� �Ǿ� ���� ����
        gameClear = false; // ������ ��ư ���ؼ� �ʿ���
        gameOver = false; // ������ ��ư ���ؼ� �ʿ���
        countTime = 5f;
        
        if (stageTimeCor != null)
        {
            StopCoroutine(stageTimeCor);
            stageTimeCor = null;
        }
        gameTime = 0;
        SetKillMonster(0);
        monsterGoal = 0;
        player.PassiveCorReset();
        UiManager.Instance.wating.SetActive(true);
        UiManager.Instance.SetGameUI(); //�����Ҷ� �ѹ��� ���������
        isRunTime = true;
        if (runTimeCor == null)
        {
            runTimeCor = StartCoroutine(RunTime());
        }
        LockCursor(false);
    }

    public void startGame() // ���� �������� ���� ����
    {
        MonsterManager.Instance.CleanMonster();// �� �ȵǼ� �ѹ���
        ItemManager.Instance.ReturnAllObjectToPool(); // �� �ȵǼ� �ѹ���
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
        gameRound = (countGame / maxStage) + 1;
        gameStage = countGame % maxStage;
        if (gameStage == 0)
        {
            gameStage = 5;
        }
        
        monsterGoal = countGame * 10;
        UiManager.Instance.SetGameUI(); //�����Ҷ� �ѹ��� ���������
        player.DoPassive();

        LockCursor(true);

        if (gameStage != 5)
        {
            MonsterManager.Instance.SpawnMonster();
        }
        else
        {
            boss.Init();
            boss.StartWeakCor();
            monsterGoal = 1;
        }
    }
    #region �ð�
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
    IEnumerator GameTime()
    {
        while (stageStart)
        {
            gameTime += 1;
            UiManager.Instance.count.text = string.Format("{0:N2}", gameTime.ToString());
            yield return new WaitForSeconds(1f);
        }
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

    #endregion
    public void AddKillMonster(int count = 1)
    {
        killMonster += count;
        UiManager.Instance.SetGameUI();

        if (killMonster >= monsterGoal)
        {
            if (!gameClear)
            {
                GoWatingRoom();
                countGame++;
            }
        }
    }

    public void LockCursor(bool cursorLock)
    {
        if (cursorLock && StopNum == 0)
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

   


    public void StopBGM()
    {
        audioSource.Stop();
    }
}