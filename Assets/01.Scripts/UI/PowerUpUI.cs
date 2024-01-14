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
    public string[] panelName = new string[3] { "�÷��̾� �ɷ�ġ", "������", "��ų" };

    public void Init() // �ʱ� ���� �� �ѹ��� �ϴ°�
    {
        SpwanPanel();
        for (int i = 0; i < panelUIs.Length; i++)
        {
            panelUIs[i].MatchingUI();
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
    

    public void SetPanelDate(int indexNum, string color, Sprite image, string accountText, int money)
    {
        panelUIs[indexNum].SetDate(color,image, accountText, money);
    }
    public void SetPanelDataNoSprite(int indexNum, string color, string accountText, int money)
    {
        panelUIs[indexNum].SetDataWithoutSprite(color,accountText, money);
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

    //public void SetPowerUpUIData()
    //{
    //    for (int i = 0; i < panelUIs.Length; i++)
    //    {

    //    }
    //}
}
