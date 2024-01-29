using System.Collections;
using UnityEngine;
//넉백 ,죽을때 사라지는거 문제 고치기
// 파워업 판넬 다시시작시 복제됨 

public class GameManager : Singleton<GameManager>
{
    public GameObject pools;
    public GameObject playerPrefab;
    public Player player { get; private set; }
    public Boss boss { get; private set; }
    public bool isNew { get; private set; } // 저장한거 불러오는지, 처음인지
    public bool isRunTime { get; private set; } // 버튼 글자 바꾸기 위해 선언
    public bool stageStart { get; private set; } // 몬스터 나오는게 스테이지 스타트
    public bool gameOver { get; private set; }
    public bool gameClear { get; private set; }

    public int monsterGoal { get; private set; }
    public int killMonster { get; private set; }
    public int countGame { get; private set; } //이걸 나누고 몫과 나머지 gameRound, gameStage , 1부터 시작
    private int maxStage = 5; // 1라운드당 5스테이지라서
    public int gameRound { get; private set; } //gameRound - gameStage 형식
    public int gameStage { get; private set; }
    Coroutine runTimeCor = null; // 5초에서 줄어듬
    Coroutine stageTimeCor = null; // 0초에서 늘어남
    public AudioSource audioSource { get; private set; }

    public float gameTime { get; private set; }
    public float countTime { get; private set; }

    public GameObject bossObj;
    public int StopNum; // 게임 정지와 커서가 다른 창에 의해서 풀려버리는 걸 방지, 0일떄만 작동되게

    #region 초기화 관련
    public void SetUpOnce() // 처음 설정할것
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

        for (int i = 1; i < UiManager.Instance.canvas.transform.childCount; i++) // 0은 eventSystem
        {
            UiManager.Instance.canvas.transform.GetChild(i).gameObject.SetActive(false);
        }
        UiManager.Instance.canvas.transform.GetChild(0).gameObject.SetActive(true);
        UiManager.Instance.startScene.SetActive(true);
    }
    public void Recycle() // 재사용할때 
    {
        //audioSource.Play();

    }
    public void Deactivate() //비활성화 할때
    {

    }
    public void DontUse() // 안쓰게 될때
    {

    }
    #endregion

    #region Set함수
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
        MonsterManager.Instance.MakeMonster(); // 오브젝트 풀만 생성
        ItemManager.Instance.Init(); // 드랍될아이템 오브젝트풀생성
        InventoryManager.Instance.Init(); // 인벤토리 아이템 칸 생성
        player.CalcPlayerStat();
        UiManager.Instance.FirstSet();
        stageStart = false;
        boss.FirstStart();
        boss.gameObject.SetActive(false);
    }

    

    public void GoWatingRoom() // 숫자 카운팅 되는 웨이팅룸 들어갈때
    {
        ItemManager.Instance.ReturnAllObjectToPool();
        MonsterManager.Instance.StopSpawnMonster();
        MonsterManager.Instance.CleanMonster();//=>딱 살아있던 애들만 죽임. (단순히 죽임. 
        stageStart = false; // 이게 되야 게임 시작
        gameClear = false; // 마지막 버튼 위해서 필요함
        gameOver = false; // 마지막 버튼 위해서 필요함
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
        UiManager.Instance.SetGameUI(); //시작할때 한번은 보여줘야함
        isRunTime = true;
        if (runTimeCor == null)
        {
            runTimeCor = StartCoroutine(RunTime());
        }
        LockCursor(false);
    }

    public void startGame() // 게임 스테이지 들어갈떄 설정
    {
        MonsterManager.Instance.CleanMonster();// 잘 안되서 한번더
        ItemManager.Instance.ReturnAllObjectToPool(); // 잘 안되서 한번더
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
        UiManager.Instance.SetGameUI(); //시작할때 한번은 보여줘야함
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
    #region 시간
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