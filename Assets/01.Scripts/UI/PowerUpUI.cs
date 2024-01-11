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
    
    public void Init() // �ʱ� ���� �� �ѹ��� �ϴ°�
    {
        SpwanPanel();
        for (int i = 0; i < panelUIs.Length; i++)
        {
            panelUIs[i].MatchingUI();
            panelUIs[i].SetPanelName(UiManager.Instance.RetrunPanelName(i));
        }
    }
    public void SpwanPanel() // �����
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
            Time.timeScale = 0f; // �ð��� �帧�� ����  //�ڷ�ƾ �ȵǰ�, ������Ʈ �� �ǰ� , �Ƚ��� ����, �巡�׵� ����
            this.gameObject.SetActive(true);
            // ������ ���� �ؾ���######3
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
