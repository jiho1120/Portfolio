using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class UIManager : Singleton<UIManager>
{
    [Header("StartUI")]
    public GameObject StartUI;
    public Text newPlayerName;    // ���� �Էµ� �÷��̾��� �г���
    public GameObject namePanel;    // �÷��̾� �г��� �Է�UI
    public Text[] slotText;        // ���Թ�ư �Ʒ��� �����ϴ� Text��

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
    public void OnNamePanel()    // �÷��̾� �г��� �Է� UI�� Ȱ��ȭ�ϴ� �޼ҵ�
    {
        newPlayerName.text = "";
        namePanel.gameObject.SetActive(true);
    }
    #endregion MenuUI

    #region Waiting
    public void SetWaitingUI()
    {
        SetOnOffTimer();
        Skip.text = "�ǳ� �ٱ�";
        Count.text = GameManager.Instance.countTime.ToString();
        if (GameManager.Instance.countTime <= 1)
        {
            Count.text = "���� ����";
        }
    }
    public void SetOnOffTimer()
    {
        if (GameManager.Instance.isCountTime)
        {
            Stop.text = "Ÿ�̸� ����";
        }
        else
        {
            Stop.text = "Ÿ�̸� ����";
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
        // ���� ī��Ʈ�� UI�� �ݿ�
        monsterCountText.text = count.ToString();
    }
    public void UpdateMonsterGoalCount(int count)
    {
        // ���� ī��Ʈ�� UI�� �ݿ�
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
