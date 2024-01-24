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
            Time.timeScale = 0f; // �ð��� �帧�� ����  //�ڷ�ƾ �ȵǰ�, ������Ʈ �� �ǰ� , �Ƚ��� ����, �巡�׵� ����
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
        UiManager.Instance.SetGameUI(); //�����ϰ� �� ����


    }

}
