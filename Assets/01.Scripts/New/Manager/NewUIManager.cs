using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;
using UnityEngine;
using UnityEngine.UI;

public class NewUIManager : Singleton<NewUIManager>
{
    [Header("StartUI")]
    public GameObject StartUI;
    public GameObject inputNamePanel;
    public InputField inputField;

    [Header("MenuUI")]
    public GameObject MenuUI;


    [Header("Waiting")]
    public GameObject WaitingUI;
    public Text Stop;
    public Text Skip;
    public Text Count;

    [Header("InGame")]
    public GameObject InGameUI;
    public Text RunTime;



    public void OnStartUI()
    {
        StartUI.gameObject.SetActive(true);
        inputNamePanel.gameObject.SetActive(false);
    }

    public void SetStopTimer()
    {
        if (NewGameManager.instance.isCountTime)
        {
            Stop.text = "타이머 멈춤";
        }
        else
        {
            Stop.text = "타이머 시작";
        }
    }
    public void SetWaitingUI()
    {
        SetStopTimer();
        Skip.text = "건너 뛰기";
        Count.text = NewGameManager.Instance.countTime.ToString();
        if (NewGameManager.Instance.countTime <= 1)
        {
            Count.text = "게임 시작";
        }
    }
}
