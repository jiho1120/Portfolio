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
    
    public void Init() // 초기 세팅 딱 한번만 하는것
    {
        SpwanPanel();
        for (int i = 0; i < panelUIs.Length; i++)
        {
            panelUIs[i].MatchingUI();
            panelUIs[i].SetPanelName(UiManager.Instance.RetrunPanelName(i));
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
    public void SetPanelName(int indexNum,string text)
    {
        panelUIs[indexNum].SetPanelName(text);
    }
    public void SetPanelDate(int indexNum,Sprite image, string accountText, string money)
    {
        panelUIs[indexNum].SetDate(image, accountText, money);
    }
    
    public void ScreenOnOff()
    {
        isOn = !isOn;
        if (isOn)
        {
            Time.timeScale = 0f; // 시간의 흐름이 멈춤  //코루틴 안되고, 업데이트 안 되고 , 픽스드 가능, 드래그도 가능
            this.gameObject.SetActive(true);
            // 데이터 세팅 해야함######3
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Time.timeScale = 1f;
            this.gameObject.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}
