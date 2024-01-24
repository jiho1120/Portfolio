using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PowerUpUI : MonoBehaviour
{
    public PanelUI[] panelUIs;
    public Transform PanelParent; //mainPanel
    public PanelUI PanelPrefab;
    public Button closeButton;
    bool isOn = false;
    int panelCount = 3;
    public string[] panelName = new string[3] { "플레이어 능력치", "아이템", "스킬" };

    public void Init() // 초기 세팅 딱 한번만 하는것
    {
        SpwanPanel();
        for (int i = 0; i < panelUIs.Length; i++)
        {
            panelUIs[i].MatchingUI();
        }
    }
    public void SpwanPanel() // 만들기
    {
        panelUIs = new PanelUI[panelCount];
        for (int i = 0; i < panelCount; i++)
        {
            PanelUI obj = Instantiate(PanelPrefab, PanelParent);
            panelUIs[i] = obj.GetComponent<PanelUI>();
        }
    }

    public void SetPanelData(int indexNum, string setType, string name, float effect, int money)
    {
        panelUIs[indexNum].SetPanelDate(setType,name, effect, money);
        panelUIs[indexNum].SetPanelIcon(indexNum,name);

    }


    public void SetPanelUINoSprite(int indexNum, string color, string accountText, int money)
    {
        panelUIs[indexNum].SetPanelUINoSprite(color,accountText, money);
    }

    public void ScreenOnOff()
    {
        isOn = !isOn;
        if (isOn)
        {
            GameManager.Instance.StopNum++;
            Time.timeScale = 0f; // 시간의 흐름이 멈춤  //코루틴 안되고, 업데이트 안 되고 , 픽스드 가능, 드래그도 가능
            gameObject.SetActive(true);
            GameManager.Instance.LockCursor(false);

        }
        else
        {
            GameManager.Instance.StopNum--;
            if (GameManager.Instance.StopNum == 0)
            {
                Time.timeScale = 1f;
            }
            gameObject.SetActive(false);
            if (GameManager.Instance.stageStart)
            {
                GameManager.Instance.LockCursor(true);
            }
        }
        UiManager.Instance.SetGameUI(); //선택하고 돈 적용


    }

}
