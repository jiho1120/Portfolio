using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UiManager : Singleton<UiManager>
{
    public PlayerConditionUI playerConditionUI;
    public Image fakeIcon;
    public GraphicRaycaster graphicRaycaster;
    public PowerUpUI powerUpUI;


    public void Init()
    {
        playerConditionUI.Init();
        powerUpUI.Init();
        SetPanelName();
    }
    public void SetUI()
    {
        playerConditionUI.SetUI();

    }
    public void ShowPowerUpPanel()
    {
        // UI 데이터 세팅
        SetPanelData();
        // 고르거나 나가기 버튼 누르면 나가기
        PowerUpScreenOnOff();
    }
    public void PowerUpScreenOnOff()
    {
        powerUpUI.ScreenOnOff();

    }
    public void SetUseSKillCoolImg(int _num)
    {
        int num = _num - 1;
        if (num == 3)
        {
            playerConditionUI.skill[num].SetBeggginSuper();
        }
        else
        {
            playerConditionUI.skill[num].SetUseSKillTime();
        }
    }
    public void SetPanelName()
    {
        for (int i = 0; i < powerUpUI.panelName.Length; i++)
        {
            powerUpUI.panelUIs[i].SetPanelName(powerUpUI.panelName[i]);
        }
    }
    public void SetPanelData()
    {
        for (int i = 0; i < powerUpUI.panelUIs.Length; i++)
        {
            string accountText = "";
            List<int> pList = new List<int>();
            ItemGrade itemGrade = ResourceManager.Instance.XMLAccess.Randomgrade(); // 등급 뽑아서
            if (i == 0)
            {
                for (int j = 0; j < ResourceManager.Instance.XMLAccess.powerUpPlayerList.Count; j++)
                {
                    if (ResourceManager.Instance.XMLAccess.powerUpPlayerList[j].grade == itemGrade.grade) // 같은 등급인것만 넣고
                    {
                        pList.Add(j);
                    }
                }
                int num = Random.Range(0, pList.Count); //걍 등급 같은 능력치중 아무거나
                num = pList[num];
                PowerUpPlayer p = ResourceManager.Instance.XMLAccess.powerUpPlayerList[num]; //리스트중에 하나 뽑음
                accountText = $"{p.statName}을{p.powerUpSize}만큼 강화한다";

            }
            else if (i == 1)
            {
                for (int j = 0; j < ResourceManager.Instance.XMLAccess.powerUpItemList.Count; j++)
                {
                    if (ResourceManager.Instance.XMLAccess.powerUpItemList[j].grade == itemGrade.grade) // 같은 등급인것만 넣고
                    {
                        pList.Add(j);
                    }
                }
                int num = Random.Range(0, pList.Count); //걍 등급 같은 능력치중 아무거나
                num = pList[num];
                PowerUpItem p = ResourceManager.Instance.XMLAccess.powerUpItemList[num]; //리스트중에 하나 뽑음
                accountText = $"{p.itemName}을{p.powerUpSize}만큼 강화한다";

            }
            else if (i == 2)
            {
                for (int j = 0; j < ResourceManager.Instance.XMLAccess.powerUpSkillList.Count; j++)
                {
                    if (ResourceManager.Instance.XMLAccess.powerUpSkillList[j].grade == itemGrade.grade) // 같은 등급인것만 넣고
                    {
                        pList.Add(j);
                    }
                }
                int num = Random.Range(0, pList.Count); //걍 등급 같은 능력치중 아무거나
                num = pList[num];
                PowerUpSkill p = ResourceManager.Instance.XMLAccess.powerUpSkillList[num]; //리스트중에 하나 뽑음

                accountText = $"{p.skillName}을{p.powerUpSize}만큼 강화한다";
            }
            Debug.Log($"{i}, {itemGrade.color}, {accountText}, {itemGrade.money}");
            powerUpUI.SetPanelDataNoSprite(i, itemGrade.color, accountText, itemGrade.money);
        }
    }

    public void ScreenOnOff(char key)
    {
        switch (key)
        {
            case 'i':
                InventoryManager.Instance.InvenOnOff();
                break;
            case 'o':
                powerUpUI.ScreenOnOff();
                break;
            default:
                break;
        }
    }

}
