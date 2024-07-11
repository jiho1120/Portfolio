using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    [Header("StartUI")]
    public GameObject StartUI;
    public Text newPlayerName;    // ���� �Էµ� �÷��̾��� �г���
    public GameObject inputName;    // �÷��̾� �г��� �Է�UI
    public InputField playerName;
    public Text[] slotText;        // ���Թ�ư �Ʒ��� �����ϴ� Text��

    [Header("BasicUI")]
    public GameObject MenuUI;
    public GameObject PopUp;
    public UIUserInfo UserInfo;
    public bool onMenu = false;

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
    public Text PlayerGoldText;


    [Header("InvenUI")]
    public GameObject Inventory;
    public UIGridScrollViewDic uIGridScrollViewDic;
    bool scrollview = false;
    public UIEquipmentView equipmentView;
    
    [Header("PlayerUI")]
    public UIPlayer uIPlayer;

    [Header("PowerUpUI")]
    public PowerUpUI powerUpUI;

    [Header("EndPanelUI")]
    public EndPanel bossEndPanel;
    public EndPanel playerEndPanel;

    [Header("BossUI")]
    public UIBoss uIBoss;



    public void InitUI()
    {
        StartUI.gameObject.SetActive(true);
        WaitingUI.gameObject.SetActive(false);
        MenuUI.gameObject.SetActive(false);
        uIPlayer.gameObject.SetActive(false);
        InGameUI.gameObject.SetActive(false);
        uIBoss.gameObject.SetActive(false);

        onMenu = false; // ���⼭ ���� �ʱ�ȭ �ϱ�
        scrollview = false;

    }
    public void InitWaitingUI()
    {
        OffStartUI();
        WaitingUI.gameObject.SetActive(true);
        SetOnOffTimer();
        Skip.text = "�ǳ� �ٱ�";
        SetWatingTimeUI();
        
    }
    public void SetInitGameUI()
    {
        WaitingUI.gameObject.SetActive(false);
        InGameUI.gameObject.SetActive(true);
        SetPlayerUI();
        UpdateMonsterGoalCount(
DataManager.Instance.gameData.killGoal);
        UpdateMonsterCount(GameManager.Instance.killMon);
        UpdatePlayerGold(GameManager.Instance.player.Stat.money);
    }

    #region BasicUI

    public void OffStartUI()
    {
        StartUI.gameObject.SetActive(false);
        inputName.gameObject.SetActive(false);
    }
    public void StartPopCor(string text, float time)
    {
        PopUp.gameObject.SetActive(true);
        PopUp.GetComponent<CorPopUp>().PopCor(text, time);
    }
    public void OnNamePanel() // �÷��̾� �г��� �Է� UI�� Ȱ��ȭ�ϴ� �޼ҵ�
    {
        newPlayerName.text = "";
        playerName.text = "";
        inputName.gameObject.SetActive(true);
    }
    public void OnOffMenu()
    {
        onMenu = !onMenu;
        if (onMenu)
        {
            GameManager.Instance.VisibleCursor();
        }
        else
        {
            GameManager.Instance.LockedCursor();
        }

        MenuUI.SetActive(onMenu);
    }
    #endregion MenuUI

    #region Waiting

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
    public void SetWatingTimeUI()
    {
        Count.text = GameManager.Instance.countTime.ToString();
        if (GameManager.Instance.countTime <= 1)
        {
            Count.text = "���� ����";
        }
    }
    #endregion

    #region InGame
    
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
    public void UpdatePlayerGold(int count)
    {
        // ���� ī��Ʈ�� UI�� �ݿ�
        PlayerGoldText.text = count.ToString();
    }
    #endregion

    #region Inventory
    public void OnOffInventory()
    {
        scrollview = !scrollview;

        Inventory.gameObject.SetActive(scrollview);
        if (scrollview)
        {
            equipmentView.Init();
            uIGridScrollViewDic.scrollView.Refresh();
            GameManager.Instance.VisibleCursor();
        }
        else
        {
            GameManager.Instance.LockedCursor();
        }
    }
    #endregion 

    #region PlayerUI
    public void SetPlayerUI()
    {
        uIPlayer.gameObject.SetActive(true);
        uIPlayer.SetPlayerUI();
    }
    public void SetPlayerHPUI()
    {
        uIPlayer.SetHPUI();
    }
    public void SetPlayerMPUI()
    {
        uIPlayer.SetMPUI();
    }
    public void SetPlayerEXPUI()
    {
        uIPlayer.SetEXPUI();
    }
    public void SetPlayerUltimateUI()
    {
        uIPlayer.SetUltimateUI();
    }

    #endregion

    #region �ǳ�
    public void PowerUpPanelOn()
    {
        powerUpUI.Active();
    }

    public void ActiveBossEndPanel()
    {
        bossEndPanel.Active();
    }
    public void ActivePlayerEndPanel()
    {
        playerEndPanel.Active();
    }
    #endregion

}
