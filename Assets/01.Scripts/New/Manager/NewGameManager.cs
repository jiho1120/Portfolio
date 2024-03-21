using System;
using System.Collections;
using UnityEngine;
/*�κ��丮 ����

���� ��ȯ
���� ��ȯ
��ų ����*/
public class NewGameManager : Singleton<NewGameManager>
{
    public bool onMenu = false;
    public GameObject playerPrefab;
    public PlayerCon player { get; private set; }

    #region Wating
    Coroutine runTimeCor = null; // 5�ʿ��� �پ��

    public bool isCountTime { get; private set; } // ��ư ���� �ٲٱ� ���� ����
    public float countTime { get; private set; } // ī��Ʈ ���°�
    public bool stageStart { get; private set; } // ���� �����°� �������� ��ŸƮ
    #endregion

    #region InGame
    public float runTime { get; private set; } // �÷��� �ð�
    
    #endregion


    void Start()
    {
        DataManager.Instance.select.Init();
        NewUIManager.Instance.OnStartUI();
        player = Instantiate(playerPrefab,transform).GetComponent<PlayerCon>();
        player.gameObject.SetActive(false);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            onMenu = !onMenu;
                NewUIManager.Instance.MenuUI.SetActive(onMenu);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            DataManager.Instance.SaveData();
        }
    }
    #region Startȭ��
   
    #endregion


    #region Waiting
    public void InitWating()
    {
        isCountTime = true;
        countTime = 5;
        NewUIManager.Instance.SetWaitingUI();
        if (runTimeCor == null)
        {
            runTimeCor = StartCoroutine(RunTime());
        }
    }
    public void DeactivateWating()
    {
        isCountTime = false;
        countTime = 5;
        NewUIManager.Instance.SetWaitingUI();
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
                NewUIManager.Instance.SetWaitingUI();
                if (countTime <= 0)
                {
                    stageStart = true;
                    NewUIManager.Instance.WaitingUI.SetActive(false );
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
        NewUIManager.Instance.SetStopTimer();
    }
    public void SkipTime()
    {
        countTime = 0;
        stageStart = true;
        runTime = 0;
        NewUIManager.Instance.SetWaitingUI();
    }
    #endregion
    #region InGame
    public void InGame()
    {
        player.gameObject.SetActive(true);
    }

    IEnumerator GameTime()
    {
        while (stageStart)
        {
            runTime += 1;
            NewUIManager.Instance.RunTime.text = string.Format("{0:N2}", runTime.ToString());
            yield return new WaitForSeconds(1f);
        }
    }
    #endregion


    #region

    #endregion


}
