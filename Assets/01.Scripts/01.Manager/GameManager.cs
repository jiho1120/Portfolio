using System.Collections;
using UnityEngine;
/* ai 강의 듣고 밀리는 현상 고치기
보스 소환
스킬 수정*/
public class GameManager : Singleton<GameManager>
{
    public GameObject playerPrefab;
    public Player player { get; private set; }


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
        ResourceManager.Instance.Init();
        ItemManager.Instance.Init();
        GridScrollViewMain.Instance.Init();
        for (int i = 0; i < UIManager.Instance.uIPlayer.uIPosionSlots.Length; i++)
        {
            UIManager.Instance.uIPlayer.uIPosionSlots[i].SetUseSlotChar($"{i + 1}");
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
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            UIManager.Instance.uIPlayer.uIPosionSlots[0].UsePosion();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            UIManager.Instance.uIPlayer.uIPosionSlots[1].UsePosion();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            UIManager.Instance.uIPlayer.uIPosionSlots[2].UsePosion();
        }
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
        player.gameObject.SetActive(true);
        UIManager.Instance.InitInGame();
        if (DataManager.Instance.gameData.gameStage != 5)
        {
            MonsterManager.Instance.Init();
            if (gameTimeCor == null)
            {
                gameTimeCor = StartCoroutine(GameTime());

            }
        }
        else
        {

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
        gameTimeCor = null;
        killMon = 0;
        DataManager.Instance.gameData.killGoal += 10;
        DataManager.Instance.gameData.gameStage += 1;
        if (DataManager.Instance.gameData.gameStage % 5 == 1)
        {
            DataManager.Instance.gameData.gameRound += 1;
        }
        MonsterManager.Instance.StopSpawnObject();
        MonsterManager.Instance.SetMonsterDeactive();
        yield return new WaitForSeconds(1f);
        ItemManager.Instance.AllItemDeActive();
        InitWating();
        stageClearCor = null;
        DataManager.Instance.Save();///// 클리어시 저장
    }

    #endregion


    #region

    #endregion


}
