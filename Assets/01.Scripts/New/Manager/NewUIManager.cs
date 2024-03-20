using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;
using UnityEngine;
using UnityEngine.UI;

public class NewUIManager : Singleton<NewUIManager>
{
    [Header("StartUI")]
    public GameObject StartUI;
    public Text newPlayerName;    // 새로 입력된 플레이어의 닉네임
    public GameObject create;    // 플레이어 닉네임 입력UI
    public Text[] slotText;        // 슬롯버튼 아래에 존재하는 Text들

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
    }

    public void SetStopTimer()
    {
        if (NewGameManager.Instance.isCountTime)
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
    public void Create()    // 플레이어 닉네임 입력 UI를 활성화하는 메소드
    {
        create.gameObject.SetActive(true);
    }
}
