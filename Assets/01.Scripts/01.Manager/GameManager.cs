using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 문제점
// 또 같은 팔로 공격
// 중력 효과 고치기
// 그라운드 쓰면 밀치기
//오늘 할일
//저장 
//네비게이션 스탑오류 고치기(내일)

public class GameManager : Singleton<GameManager>
{
    public GameObject pools;
    public bool gameOver { get; private set; }
    public bool gameClear { get; private set; }
    public GameObject playerPrefab;
    public Player player;
    public bool gameStart;
    public bool isRunTime = true; // 버튼 글자 바꾸기 위해 선언
    public int monsterGoal = 0;
    public int killMonster = 0;
    private int countGame = 0; //이걸 나누고 몫과 나머지 gameRound, gameStage
    private int maxStage = 6; // 1라운드당 5스테이지라서
    public int gameRound = 0; //gameRound - gameStage 형식
    public int gameStage = 0;
    Coroutine startGameCor = null;
    public Coroutine goWatingRoom = null;
    Coroutine passiveCor = null;
    Coroutine runTimeCor = null;


    public float gameTime { get; private set; }
    public float countTime { get; private set; }

    public Boss boss { get; private set; }
    public GameObject bossObj;
    public GameObject canvas;

    private void Start()
    {
        SceneLoadController.Instance.GoStartScene();


        //DoItOnceMain();
    }
    public void DoItOnceMain()
    {
        //player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        ResourceManager.Instance.LoadResources();
        player.gameObject.SetActive(true);
        gameClear = false;
        gameOver = false;
        player.FirstStart();
        SkillManager.Instance.Init();
        MonsterManager.Instance.Init();
        InventoryManager.Instance.Init();
        ItemManager.Instance.Init();
        player.CalcPlayerStat();
        UiManager.Instance.FirstSet();
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
        //UiManager.Instance.SetUI();
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

        if (Input.GetKeyDown(KeyCode.KeypadPlus))
        {
            killMonster += 1;
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            player.SetHp(0);
        }

    }
    public void LoadStart()
    {
        player.gameObject.SetActive(false);
        pools.SetActive(false);
        canvas.SetActive(true);
        for (int i = 1; i < canvas.transform.childCount; i++) // 0은 eventSystem
        {
            canvas.transform.GetChild(i).gameObject.SetActive(false);
        }
        canvas.transform.GetChild(0).gameObject.SetActive(true);
        UiManager.Instance.startScene.SetActive(true);
    }

    public void LoadMain()
    {
        if (player == null)
        {
            player = Instantiate(playerPrefab).GetComponent<Player>();
        }
        player.gameObject.SetActive(true);
        pools.SetActive(true);
        canvas.SetActive(true);
        DoItOnceMain();
    }
    public void SetGameOver()
    {
        gameOver = true;
    }
    public void SetGameClear(bool clear = true)
    {
        gameClear = clear;
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
        MonsterManager.Instance.CleanMonster();//=>딱 살아있던 애들만 죽임. (단순히 죽임. 
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
            PassiveSkill ps =  CallPassiveSkill(isPlayer);
            yield return new WaitForSeconds(ps.skillStat.duration);
        }
    }
    public PassiveSkill CallPassiveSkill(bool isPlayer)
    {
        if (SkillManager.Instance.passiveSkill != null)
        {
            SkillManager.Instance.passiveSkill.DoReset();

        }
        PassiveSkill ps =  SkillManager.Instance.CallPassiveSkill(isPlayer);
        return ps;
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