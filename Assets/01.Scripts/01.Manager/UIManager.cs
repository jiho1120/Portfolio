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

    [Header("MenuUI")]
    public GameObject MenuUI;

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


    #region StartUI
    public void OnStartUI()
    {
        StartUI.gameObject.SetActive(true);
    }
    public void OffStartUI()
    {
        StartUI.gameObject.SetActive(false);
        namePanel.gameObject.SetActive(false);
    }
    public void OnNamePanel()    // �÷��̾� �г��� �Է� UI�� Ȱ��ȭ�ϴ� �޼ҵ�
    {
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
        UpdateMonsterGoalCount(GameManager.Instance.killGoal);
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
