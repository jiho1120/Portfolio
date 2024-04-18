using UnityEngine;
using UnityEngine.UI;

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
    public UIUserInfo UserInfo;

    [Header("InvenUI")]
    public GameObject Inventory;
    public UIGridScrollViewDic uIGridScrollViewDic;
    bool scrollview = false;
    public EquipSlot[] equipSlots;


    [Header("Waiting")]
    public GameObject WaitingUI;
    public Text Stop;
    public Text Skip;
    public Text Count;

    [Header("InGame")]
    public GameObject InGameUI;
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
        PopUp.GetComponent<CorPopUp>().PopCor(text, time);
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


    #endregion

    #region Inventory
    public void setequip()
    {
        for (int i = 0; i < equipSlots.Length; i++)
        {
            equipSlots[i].SetEquipSlot();
        }
    }

    public void OnOffInventory()
    {
        scrollview = !scrollview;
        
        Inventory.gameObject.SetActive(scrollview);
        if (scrollview == true)
        {
            setequip();
            uIGridScrollViewDic.scrollView.Refresh();
        }

    }
    #endregion 

    #region

    #endregion

    #region

    #endregion




}
