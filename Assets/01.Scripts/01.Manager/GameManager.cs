using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 문제점
// 또 같은 팔로 공격

//오늘 할일
// 중력 효과 고치기
// 그라운드 쓰면 밀치기
// 능력치 아이템 스킬 강화 선택 // 이 방법 외에는 어떤 경우에도 강화 불가
// 레벨업 함수 완성하기
// 레벨업 화면 버튼 기능들 만들기
// 레벨업 데이터 만들고 넣기

public class GameManager : Singleton<GameManager>
{
    public Player player;
    public bool gameStart = false;
    public bool runTime = true;
    public int monsterGoal = 0;
    public int killMonster = 0;
    private int countGame = 0; //이걸 나누고 몫과 나머지이 gameRound, gameStage
    private int maxStage = 6; // 1라운드당 5스테이지라서
    public int gameRound=0; //gameRound - gameStage 형식
    public int gameStage=0;
    public bool cursorLock = false;

    public float gameTime { get; private set; }
    public float countTime { get; private set; }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        ResourceManager.Instance.LoadResources();
        player.FirstStart();
        //player.CalcPlayerStat();
        SkillManager.Instance.Init();
        MonsterManager.Instance.Init();
        InventoryManager.Instance.Init();
        ItemManager.Instance.Init();
        player.Init();
        UiManager.Instance.Init();
        //ResourceManager.Instance.XMLAccess.ShowListInfo();
        countTime = 5f;
        UiManager.instance.wating.SetActive(true);
        InvokeRepeating("CallPassiveSkill", 5.0f, 10.0f);
    }
    private void Update()
    {
        UiManager.Instance.SetUI();
        if (countTime > 0 && runTime)
        {
            countTime -= Time.deltaTime;
        }
        if (countTime <= 0)
        {
            gameStart = true;
        }
        if (gameStart)
        {
            LockCursor();
            runTime = false;
            gameTime += Time.deltaTime;
            startGame();
        }
    }
    public void startGame()
    {
        gameRound = (countGame / maxStage) + 1;
        gameStage = (countGame % maxStage) +1;
        monsterGoal = gameRound * 10;
        UiManager.instance.wating.SetActive(false);
        
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
            // 보스 생성
        }
    }
    public void LockCursor()
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

    public void StopOnOffTime()
    {
        runTime = !runTime;
    }
    public void SkipTime()
    {
        countTime = 0;
        gameStart = true;
        gameTime = 0;
    }
    public void CallPassiveSkill()
    {
        if (SkillManager.Instance.passiveSkill != null)
        {
            SkillManager.Instance.passiveSkill.DoReset();

        }
        SkillManager.Instance.CallPassiveSkill();
    }
}