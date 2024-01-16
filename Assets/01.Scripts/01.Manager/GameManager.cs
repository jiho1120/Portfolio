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
            gameTime += Time.deltaTime;
            startGame();
        }
    }
    public void startGame()
    {
        gameRound = (countGame / maxStage) + 1;
        gameStage = (countGame % maxStage) +1;
        UiManager.instance.wating.SetActive(false);
        //Cursor.lockState = CursorLockMode.Locked; // 씬매니저에서 들어올때 한번
        //Cursor.visible = false;
        if (gameStage == 5)
        {
            MonsterManager.Instance.SpawnMonster();
        }
        // 끝나면 countGame 숫자 올리기
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
}