using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewGameManager : Singleton<NewGameManager>
{
    public int nowGameIdx { get; private set; } // ����� �ε��� ��ȣ
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
        NewUIManager.Instance.OnStartUI();
        player = Instantiate(playerPrefab).GetComponent<PlayerCon>();
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
    }
    #region Startȭ��
    public void SetNowGameIdx(int idx)
    {
        nowGameIdx = idx;
    }

    /// <summary>
    /// ������ ���� ���Կ� Ȯ�� ������ ���� ����� ����
    /// </summary>
    public void GameStartButton()
    {
        SceneLoadController.Instance.GoGameScene();
    }
    #endregion


    #region Waiting
    public void Wating()
    {
        isCountTime = true;
        countTime = 5;
        NewUIManager.Instance.SetWaitingUI();
        if (runTimeCor == null)
        {
            runTimeCor = StartCoroutine(RunTime());
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
                    //startGame();
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
