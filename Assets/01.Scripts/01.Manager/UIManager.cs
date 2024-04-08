using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class UIManager : Singleton<UIManager>
{
    [Header("StartUI")]
    public GameObject StartUI;
    public Text newPlayerName;    // 새로 입력된 플레이어의 닉네임
    public GameObject namePanel;    // 플레이어 닉네임 입력UI
    public Text[] slotText;        // 슬롯버튼 아래에 존재하는 Text들

    [Header("BasicUI")]
    public GameObject MenuUI;
    public GameObject PopUp;

    [Header("Waiting")]
    public GameObject WaitingUI;
    public Text Stop;
    public Text Skip;
    public Text Count;

    [Header("InGame")]
    public GameObject InGameUI;
    public GameObject UIGridScrollView;
    public UIGridScrollViewDic uIGridScrollViewDic;
    bool scrollview = false;
    public Text RunTime;
    public Text monsterCountText;
    public Text monsterGoalText;


    #region BasicUI
    public void OnStartUI()
    {
        StartUI.gameObject.SetActive(true);
    }
    public void OffStartUI()
    {
        StartUI.gameObject.SetActive(false);
        namePanel.gameObject.SetActive(false);
    }
    public void StartPopCor(string text, float time)
    {
        PopUp.gameObject.SetActive(true);
        PopUp.GetComponent<BasicPopUp>().PopCor(text, time);
    }
    public void OnNamePanel()    // 플레이어 닉네임 입력 UI를 활성화하는 메소드
    {
        newPlayerName.text = "";
        namePanel.gameObject.SetActive(true);
    }
    #endregion MenuUI

    #region Waiting
    public void SetWaitingUI()
    {
        SetOnOffTimer();
        Skip.text = "건너 뛰기";
        Count.text = GameManager.Instance.countTime.ToString();
        if (GameManager.Instance.countTime <= 1)
        {
            Count.text = "게임 시작";
        }
    }
    public void SetOnOffTimer()
    {
        if (GameManager.Instance.isCountTime)
        {
            Stop.text = "타이머 멈춤";
        }
        else
        {
            Stop.text = "타이머 시작";
        }
    }
    #endregion

    #region InGame
    public void InitInGame()
    {
        InGameUI.gameObject.SetActive(true);
        UpdateMonsterGoalCount(
DataManager.Instance.gameData.killGoal);
        UpdateMonsterCount(GameManager.Instance.killMon);
    }
    public void UpdateMonsterCount(int count)
    {
        // 몬스터 카운트를 UI에 반영
        monsterCountText.text = count.ToString();
    }
    public void UpdateMonsterGoalCount(int count)
    {
        // 몬스터 카운트를 UI에 반영
        monsterGoalText.text = count.ToString();
    }

    public void OnOffScrollView()
    {
        scrollview = !scrollview;
        if (scrollview == true)
        {
            uIGridScrollViewDic.scrollView.Refresh();
        }
        UIGridScrollView.gameObject.SetActive(scrollview);
    }
    #endregion

    #region

    #endregion

    #region

    #endregion

    #region

    #endregion




}
