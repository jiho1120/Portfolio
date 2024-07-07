using System.Collections;
using UnityEngine;
/* 
 * �и��� ���� �´� �ִϸ��̼� ����
 * �� �Ŵ°� �ʿ��Ѱ�  ���϶����� �ϱ�
 * ����������(���� await)
 *  ���� Ŭ����� ��ŸƮ�ϴ� �κ� ����ȭ�� �ٲٱ�
 *   System.Threading.Thread.Sleep(3000); 

*/
public class GameManager : Singleton<GameManager>
{
    public GameObject playerPrefab;
    public GameObject bossPrefab;

    public Player player { get; private set; }
    public Boss boss { get; private set; }
    public int CreatureId = 0;
    int cursorCount = 0; // 0�̸� Ǯ��

    #region Wating
    Coroutine runTimeCor = null; // 5�ʿ��� �پ��
    public bool isCountTime { get; private set; } // ��ư ���� �ٲٱ� ���� ����
    public float countTime { get; private set; } // ī��Ʈ ���°�
    #endregion

    #region InGame
    public bool stageStart { get; private set; } // ���� �����°� �������� ��ŸƮ
    public float runTime { get; private set; } = 0; // �÷��� �ð�
    public int killMon { get; private set; } = 0;// ���� ���� ��

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
    // �˾� ų��
    public void VisibleCursor(bool isCount = true) // ī��Ʈ�� �ð����߰� ������ true
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
    #region Startȭ��

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

        // ��ҿ��� 2���� ���̴ٰ� �������� ������ �ٷ����̱����ؼ�
        MonsterManager.Instance.SetTimeDelay(0f);

        yield return new WaitForSeconds(3f);
        //ItemManager.Instance.AllItemDeActive();
        InitWating();
        stageClearCor = null;
        DataManager.Instance.Save();///// Ŭ����� ����
    }

    #endregion


    #region

    #endregion


}
