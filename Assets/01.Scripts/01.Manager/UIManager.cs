using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public UIUserInfo UserInfo;
    public bool onMenu = false;


    [Header("InvenUI")]
    public GameObject Inventory;
    public UIGridScrollViewDic uIGridScrollViewDic;
    bool scrollview = false;
    public UIEquipmentView equipmentView;

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

    [Header("PlayerUI")]
    public UIPlayer uIPlayer;

    [Header("PowerUpUI")]
    public PowerUpUI powerUpUI;

    public void Init()
    {
        powerUpUI.Init();

    }
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
    public void OnNamePanel()    // 플레이어 닉네임 입력 UI를 활성화하는 메소드
    {
        newPlayerName.text = "";
        namePanel.gameObject.SetActive(true);
    }
    public void OnOffMenu()
    {
        onMenu = !onMenu;
        if (onMenu)
        {
            GameManager.Instance.VisibleCursor();
            //Time.timeScale = 0f;
        }
        else
        {
            GameManager.Instance.LockedCursor();
        }

        MenuUI.SetActive(onMenu);
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
        uIPlayer.gameObject.SetActive(true);
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
            //Time.timeScale = 0f;
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
        uIPlayer.SetUI();
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

    #region 판넬
    public void PowerUpPanelOn()
    {
        powerUpUI.Active();
    }
    //public void SetPanelData()
    //{
    //    for (int i = 0; i < powerUpUI.panelUIs.Length; i++)
    //    {
    //        string accountText = "";
    //        List<int> pList = new List<int>();
    //        ItemGrade itemGrade = ResourceManager.Instance.XMLAccess.Randomgrade(); // 등급 뽑아서
    //        if (i == 0)
    //        {
    //            for (int j = 0; j < ResourceManager.Instance.XMLAccess.powerUpPlayerList.Count; j++)
    //            {
    //                if (ResourceManager.Instance.XMLAccess.powerUpPlayerList[j].grade == itemGrade.grade) // 같은 등급인것만 넣고
    //                {
    //                    pList.Add(j);
    //                }
    //            }
    //            int num = Random.Range(0, pList.Count); //걍 등급 같은 능력치중 아무거나
    //            num = pList[num];
    //            PowerUpPlayer p = ResourceManager.Instance.XMLAccess.powerUpPlayerList[num]; //리스트중에 하나 뽑음
    //            powerUpUI.SetPanelData(i, "player", p.statName, p.powerUpSize, itemGrade.money);
    //            accountText = $"{p.statName}을{p.powerUpSize}만큼 강화한다";

    //        }
    //        else if (i == 1)
    //        {
    //            for (int j = 0; j < ResourceManager.Instance.XMLAccess.powerUpItemList.Count; j++)
    //            {
    //                if (ResourceManager.Instance.XMLAccess.powerUpItemList[j].grade == itemGrade.grade) // 같은 등급인것만 넣고
    //                {
    //                    pList.Add(j);
    //                }
    //            }
    //            int num = Random.Range(0, pList.Count); //걍 등급 같은 능력치중 아무거나
    //            num = pList[num];
    //            PowerUpItem p = ResourceManager.Instance.XMLAccess.powerUpItemList[num]; //리스트중에 하나 뽑음
    //            accountText = $"{p.itemName}을{p.powerUpSize}만큼 강화한다";
    //            powerUpUI.SetPanelData(i, "item", p.itemName, p.powerUpSize, itemGrade.money);

    //        }
    //        else if (i == 2)
    //        {
    //            for (int j = 0; j < ResourceManager.Instance.XMLAccess.powerUpSkillList.Count; j++)
    //            {
    //                if (ResourceManager.Instance.XMLAccess.powerUpSkillList[j].grade == itemGrade.grade) // 같은 등급인것만 넣고
    //                {
    //                    pList.Add(j);
    //                }
    //            }
    //            int num = Random.Range(0, pList.Count); //걍 등급 같은 능력치중 아무거나
    //            num = pList[num];
    //            PowerUpSkill p = ResourceManager.Instance.XMLAccess.powerUpSkillList[num]; //리스트중에 하나 뽑음

    //            accountText = $"{p.skillName}을{p.powerUpSize}만큼 강화한다";
    //            powerUpUI.SetPanelData(i, "skill", p.skillName, p.powerUpSize, itemGrade.money);
    //        }

    //        powerUpUI.SetPanelUINoSprite(i, itemGrade.color, accountText, itemGrade.money);

    //    }
    //}
    #endregion


    #region 

    #endregion

}
