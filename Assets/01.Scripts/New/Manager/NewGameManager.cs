using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewGameManager : Singleton<NewGameManager>
{
    public int nowGameIdx { get; private set; } // 저장된 인덱스 번호
    public bool onMenu = false;
    public GameObject playerPrefab;
    public PlayerCon player { get; private set; }

    #region Wating
    Coroutine runTimeCor = null; // 5초에서 줄어듬

    public bool isCountTime { get; private set; } // 버튼 글자 바꾸기 위해 선언
    public float countTime { get; private set; } // 카운트 세는거
    public bool stageStart { get; private set; } // 몬스터 나오는게 스테이지 스타트
    #endregion

    #region InGame
    public float runTime { get; private set; } // 플레이 시간
    
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
    #region Start화면
    public void SetNowGameIdx(int idx)
    {
        nowGameIdx = idx;
    }

    /// <summary>
    /// 데이터 없는 슬롯에 확인 누르면 새로 만들고 저장
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
