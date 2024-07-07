using System.Collections;
using UnityEngine;
/* 
 * 밀리고 나서 맞는 애니메이션 실행
 * 락 거는거 필요한곳  보일때마다 하기
 * 레벨업구현(동기 await)
 *  게임 클리어시 스타트하는 부분 동기화로 바꾸기
 *   System.Threading.Thread.Sleep(3000); 

*/
public class GameManager : Singleton<GameManager>
{
    public GameObject playerPrefab;
    public GameObject bossPrefab;

    public Player player { get; private set; }
    public Boss boss { get; private set; }
    public int CreatureId = 0;
    int cursorCount = 0; // 0이면 풀림

    #region Wating
    Coroutine runTimeCor = null; // 5초에서 줄어듬
    public bool isCountTime { get; private set; } // 버튼 글자 바꾸기 위해 선언
    public float countTime { get; private set; } // 카운트 세는거
    #endregion

    #region InGame
    public bool stageStart { get; private set; } // 몬스터 나오는게 스테이지 스타트
    public float runTime { get; private set; } = 0; // 플레이 시간
    public int killMon { get; private set; } = 0;// 잡은 몬스터 수

    Coroutine stageClearCor = null;
    Coroutine gameTimeCor = null;


    #endregion


    void Start()
    {
        DataManager.Instance.select.Init();
        UIManager.Instance.OnStartUI();
        player = Instantiate(playerPrefab, transform).GetComponent<Player>();
        player.gameObject.SetActive(false);

        boss = Instantiate(bossPrefab, transform).GetComponent<Boss>();
        boss.gameObject.SetActive(false);

        ResourceManager.Instance.Init();
        ItemManager.Instance.Init();
        GridScrollViewMain.Instance.Init();
        for (int i = 0; i < UIManager.Instance.uIPlayer.uiPosionSlots.Length; i++)
        {
            UIManager.Instance.uIPlayer.uiPosionSlots[i].SetUseSlotChar($"{i + 1}");
        }
        SkillManager.Instance.Init();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            UIManager.instance.OnOffMenu();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            UIManager.Instance.equipmentView.Init();
            DataManager.Instance.gameData.invenDatas.ShowDIc();
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            UIManager.Instance.OnOffInventory();
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            UIManager.Instance.UserInfo.onoff();
        }
        
    }
    public void LockedCursor(bool isCount = true)
    {
        if (isCount)
        {
            cursorCount--;
        }
        Debug.Log(cursorCount);
        if (cursorCount == 0)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Time.timeScale = 1f;
        }

    }
    // 팝업 킬때
    public void VisibleCursor(bool isCount = true) // 카우트로 시간멈추고 싶으면 true
    {
        if (isCount)
        {
            cursorCount++;
        }
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Debug.Log(cursorCount);
    }
    public void SetCursorCount(int val)
    {
        cursorCount = val;
    }
    #region Start화면

    #endregion


    #region Waiting
    public void InitWating()
    {
        UIManager.Instance.InGameUI.gameObject.SetActive(false);
        isCountTime = true;
        countTime = 5;
        UIManager.Instance.WaitingUI.gameObject.SetActive(true);
        UIManager.Instance.SetWaitingUI();
        if (runTimeCor == null)
        {
            runTimeCor = StartCoroutine(RunTime());
        }
        VisibleCursor(false);
    }
    public void DeactivateWating()
    {
        isCountTime = false;
        countTime = 5;
        UIManager.Instance.SetWaitingUI();
        if (runTimeCor != null)
        {
            StopCoroutine(runTimeCor);
            runTimeCor = null;
        }
    }
    IEnumerator RunTime()
    {
        while (true)
        {
            if (isCountTime)
            {
                yield return new WaitForSeconds(1f);
                countTime -= 1;
                UIManager.Instance.SetWaitingUI();
                if (countTime <= 0)
                {
                    stageStart = true;
                    UIManager.Instance.WaitingUI.SetActive(false);
                    InGame();
                    DeactivateWating();
                }
            }
            else
            {
                yield return null;
            }
        }
    }
    public void OnOffTime()
    {
        isCountTime = !isCountTime;
        UIManager.Instance.SetOnOffTimer();
    }
    public void SkipTime()
    {
        countTime = 0;
        stageStart = true;
        runTime = 0;
        StopCoroutine(runTimeCor);
        runTimeCor = null;
        UIManager.Instance.SetWaitingUI();
        UIManager.Instance.WaitingUI.SetActive(false);
        InGame();

    }
    #endregion

    #region InGame

    public void InGame()
    {
        DeactivateWating();
        player.Activate();
        LockedCursor(false);

        UIManager.Instance.InitInGame();
        
        if (DataManager.Instance.gameData.gameStage % 5 != 0)
        {
            MonsterManager.Instance.Init();
        }
        else
        {
            boss.Activate();
        }
        if (gameTimeCor == null)
        {
            gameTimeCor = StartCoroutine(GameTime());
        }
    }

    IEnumerator GameTime()
    {
        while (stageStart)
        {
            runTime += 1;
            UIManager.Instance.RunTime.text = string.Format("{0:N2}", runTime.ToString());
            yield return new WaitForSeconds(1f);
        }
    }

    public void SetKillMon(int count)
    {
        killMon = count;
        CheakStageClear();
    }

    public void CheakStageClear()
    {
        if (killMon >= DataManager.Instance.gameData.killGoal)
        {
            if (stageClearCor == null)
            {
                stageClearCor = StartCoroutine(StageClear());
            }
        }
    }
    IEnumerator StageClear()
    {
        stageStart = false;
        SkillManager.Instance.AllSKillDeactive(player);
        gameTimeCor = null;
        killMon = 0;
        DataManager.Instance.gameData.killGoal += 10;
        DataManager.Instance.gameData.gameStage += 1;
        if (DataManager.Instance.gameData.gameStage % 5 == 1)
        {
            DataManager.Instance.gameData.gameRound += 1;
        }

        // 평소에는 2초후 죽이다가 스테이지 끝나면 바로죽이기위해서
        MonsterManager.Instance.SetTimeDelay(0f);

        yield return new WaitForSeconds(3f);
        //ItemManager.Instance.AllItemDeActive();
        InitWating();
        stageClearCor = null;
        DataManager.Instance.Save();///// 클리어시 저장
    }

    #endregion


    #region

    #endregion


}
