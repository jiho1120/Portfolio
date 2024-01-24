using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : Singleton<GameManager>
{
    public GameObject pools;
    bool isNew = true; // 저장한거 불러오는지, 처음인지
    public bool isWating = false; // 웨이팅화면인 상태
    public bool isRunTime = true; // 버튼 글자 바꾸기 위해 선언
    public bool stageStart; // 몬스터 나오는게 스테이지 스타트
    public bool gameOver { get; private set; }
    public bool gameClear { get; private set; }
    public GameObject playerPrefab;
    public Player player;
    public Boss boss { get; private set; }

    public int monsterGoal { get; private set; }
    public int killMonster { get; private set; }
    public int countGame { get; private set; } //이걸 나누고 몫과 나머지 gameRound, gameStage
    private int maxStage = 6; // 1라운드당 5스테이지라서
    public int gameRound = 0; //gameRound - gameStage 형식
    public int gameStage = 0;
    Coroutine passiveCor = null;
    Coroutine runTimeCor = null; // 5초에서 줄어듬
    Coroutine stageTimeCor = null; // 0초에서 늘어남
    public AudioSource audioSource { get; private set; }

    public float gameTime { get; private set; }
    public float countTime { get; private set; }

    public GameObject bossObj;
    public GameObject canvas;
    public int StopNum; // 게임 정지와 커서가 다른 창에 의해서 풀려버리는 걸 방지, 0일떄만 작동되게

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
        audioSource = GetComponent<AudioSource>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        boss = GameObject.FindGameObjectWithTag("Boss").GetComponent<Boss>();
        SceneLoadController.Instance.GoStartScene();
    }
    private void Update()
    {
        
    }

    public void LoadStartScene()
    {
        audioSource.Play();
        StopNum = 0;
        player.gameObject.SetActive(false);
        pools.SetActive(false);
        canvas.SetActive(true);
        monsterGoal = 0;
        SetKillMonster(0);
        SetCountGame(0);
        for (int i = 1; i < canvas.transform.childCount; i++) // 0은 eventSystem
        {
            canvas.transform.GetChild(i).gameObject.SetActive(false);
        }
        canvas.transform.GetChild(0).gameObject.SetActive(true);
        UiManager.Instance.startScene.SetActive(true);
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
        MonsterManager.Instance.MakeMonster(); // 오브젝트 풀만 생성
        ItemManager.Instance.Init(); // 드랍될아이템 오브젝트풀생성
        InventoryManager.Instance.Init(); // 인벤토리 아이템 칸 생성
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

    public void GoWatingRoom() // 숫자 카운팅 되는 웨이팅룸 들어갈때
    {
        ItemManager.Instance.ReturnAllObjectToPool();
        MonsterManager.Instance.StopSpawnMonster();
        MonsterManager.Instance.CleanMonster();//=>딱 살아있던 애들만 죽임. (단순히 죽임. 
        stageStart = false; // 이게 되야 게임 시작
        gameClear = false; // 마지막 버튼 위해서 필요함
        gameOver = false; // 마지막 버튼 위해서 필요함
        countTime = 5f;
        isWating = true;
        
        if (stageTimeCor != null)
        {
            StopCoroutine(stageTimeCor);
            stageTimeCor = null;
        }
        gameTime = 0;
        SetKillMonster(0);
        monsterGoal = 0;
        if (passiveCor != null)
        {
            StopCoroutine(passiveCor);
            passiveCor = null;
        }
        UiManager.Instance.wating.SetActive(true);
        UiManager.Instance.SetGameUI(); //시작할때 한번은 보여줘야함
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
        SetCountGame(countGame + 1);
        gameRound = (countGame / maxStage) + 1;
        gameStage = countGame % maxStage;
        

        monsterGoal = countGame * 10;
        UiManager.Instance.SetGameUI(); //시작할때 한번은 보여줘야함

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
            UiManager.Instance.count.text = string.Format("{0:N2}", gameTime.ToString());
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

    public void StopBGM()
    {
        audioSource.Stop();
    }
    public void SetKillMonster(int num)
    {
        killMonster = num;
    }
}