using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UiManager : Singleton<UiManager>
{
    public PlayerConditionUI playerConditionUI;
    public Image fakeIcon;
    public GraphicRaycaster graphicRaycaster;

    public void Init()
    {
        playerConditionUI.Init();
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
}
