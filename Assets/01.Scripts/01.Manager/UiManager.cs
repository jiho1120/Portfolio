using System;
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

    string[] panelName = new string[3] { "플레이어 능력치", "아이템", "스킬" };

    public void Init()
    {
        playerConditionUI.Init();
        powerUpUI.Init();
    }
    public void SetUI()
    {
        playerConditionUI.SetUI();
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
    public string RetrunPanelName(int num)
    {
        return panelName[num];
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
