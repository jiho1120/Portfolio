using System.Collections;
using UnityEngine;
using UnityEngine.Pool;
public class GameManager : Singleton<GameManager>
{
    public GameObject playerPrefab;
    public GameObject bossPrefab;

    public Player player { get; private set; }
    public Boss boss { get; private set; }
    public int CreatureId;
    int cursorCount; // 0�̸� Ǯ��

    #region Wating
    Coroutine runTimeCor; // 5�ʿ��� �پ��
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
        Init();
    }

    public void Init()
    {
        ResourceManager.Instance.Init();
        DataManager.Instance.Init();
        if (player == null)
        {
            player = Instantiate(playerPrefab, transform).GetComponent<Player>();
            player.SetEnemyLayer(1 << LayerMask.NameToLayer("Enemy"));
        }
        if (boss == null)
        {
            boss = Instantiate(bossPrefab, transform).GetComponent<Boss>();
            boss.SetEnemyLayer(1 << LayerMask.NameToLayer("Player"));
        }
        player.gameObject.SetActive(false);
        boss.gameObject.SetActive(false);
        SkillManager.Instance.InitSkillDicts();
        InitHome();
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        if (stageStart)
        {
            if (Input.GetKeyDown(KeyCode.M))
            {
                UIManager.instance.OnOffMenu();
            }
            if (Input.GetKeyDown(KeyCode.I))
            {
                UIManager.Instance.OnOffInventory();
            }
            if (Input.GetKeyDown(KeyCode.U))
            {
                UIManager.Instance.UserInfo.onoff();
            }

            // �ؿ��� ġƮŰ
            if (Input.GetKeyDown(KeyCode.Z))
            {
                // ���� Ȱ��ȭ�� ��� Creature ������Ʈ�� ã���ϴ�.
                Monster[] activeMonsters = FindObjectsOfType<Monster>();

                foreach (var Monster in activeMonsters)
                {
                    Monster.SetDead(true);
                }
            }
            if (Input.GetKeyDown(KeyCode.X))
            {
                // ���� Ȱ��ȭ�� ��� Creature ������Ʈ�� ã���ϴ�.
                DroppedItem[] activeItemss = FindObjectsOfType<DroppedItem>();

                foreach (var item in activeItemss)
                {
                    item.TriggerOnPlayer(); 
                }
            }
            if (Input.GetKeyDown(KeyCode.C))
            {
                player.SetAtt(1000);
            }

        }
        

    }

    /// <summary>
    /// ������ Ŀ�� �� ���� bool �� isCount�� ������ ���ҽ�ų�� ���� true�� -1
    /// </summary>
    /// <param name="isCount"></param>
    public void LockedCursor(bool isCount = true)
    {
        if (isCount)
        {
            cursorCount--;
        }
        if (cursorCount == 0)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Time.timeScale = 1f;
        }


    }
    /// <summary>
    /// ������ Ŀ�� ���� bool �� isCount�� ������ ������ų�� ���� true�� +1
    /// </summary>
    /// <param name="isCount"></param>
    public void VisibleCursor(bool isCount = true)
    {
        if (isCount)
        {
            cursorCount++;

            Time.timeScale = 0f;
        }
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    #region Homeȭ��
    public void InitHome()
    {
        CreatureId = DataManager.Instance.gameData.creatureId;
        cursorCount = 0;
        stageStart = false;
        runTime = 0;
        if (runTimeCor != null)
        {
            StopCoroutine(runTimeCor);
            runTimeCor = null;
        }
        stageClearCor = null;
        gameTimeCor = null;
        UIManager.Instance.InitUI(); // ������ �÷��̾� ������ 
        VisibleCursor(false);
        MonsterManager.Instance.RecallMonsters();
        ItemManager.Instance.RecallAllItems();
        GridScrollViewMain.Instance.Init();

    }
    public void SetstageStart(bool isOn)
    {
        stageStart = isOn;
    }
    #endregion


    #region Waiting
    public void InitWating()
    {
        countTime = 5;
        isCountTime = true;
        killMon = 0;
        runTime = 0;
        runTimeCor = null;
        VisibleCursor(false);
        UIManager.Instance.InitWaitingUI();
        Time.timeScale = 1f;
        player.StopMpCor();
        if (runTimeCor == null)
        {
            runTimeCor = StartCoroutine(RunCountTime());
        }
    }
    
    public void DeactiveWating()
    {
        isCountTime = false;
        countTime = 5;
        if (runTimeCor != null)
        {
            StopCoroutine(runTimeCor);
            runTimeCor = null;
        }
    }
    IEnumerator RunCountTime()
    {
        while (true)
        {
            if (isCountTime)
            {
                yield return new WaitForSeconds(1f);

                countTime -= 1;
                UIManager.Instance.SetWatingTimeUI();
                if (countTime <= 0)
                {
                    stageStart = true;
                    InitGame();

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
        if (runTimeCor != null)
        {
            StopCoroutine(runTimeCor);
        }
        runTimeCor = null;
        InitGame();

    }
    #endregion

    #region InGame

    public void InitGame()
    {
        DeactiveWating();
        player.Activate();
        UIManager.Instance.SetInitGameUI();
        LockedCursor(false);

        if (DataManager.Instance.gameData.gameStage % 3 != 0)
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
        runTime = 0;
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
        SkillManager.Instance.DeactivateAllSkills(player);
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
        InitWating();
        stageClearCor = null;
        DataManager.Instance.Save();///// Ŭ����� ����
    }

    #endregion
}
