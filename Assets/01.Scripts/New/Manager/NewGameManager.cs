using System;
using System.Collections;
using UnityEngine;
/*인베토리 복붙

몬스터 소환
보스 소환
스킬 수정*/
public class NewGameManager : Singleton<NewGameManager>
{
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
    #region Start화면
   
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
