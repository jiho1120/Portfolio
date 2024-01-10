using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiManager : Singleton<UiManager>
{
    public PlayerConditionUI playerConditionUI;

    public void SetUI()
    {
        playerConditionUI.SetUI();
    }
}
