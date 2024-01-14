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


    public void Init()
    {
        playerConditionUI.Init();
        powerUpUI.Init();
        SetPanelName();
    }
    public void SetUI()
    {
        playerConditionUI.SetUI();

    }
    public void ShowPowerUpPanel()
    {
        // UI ������ ����
        SetPanelData();
        // ���ų� ������ ��ư ������ ������
        PowerUpScreenOnOff();
    }
    public void PowerUpScreenOnOff()
    {
        powerUpUI.ScreenOnOff();

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
    public void SetPanelName()
    {
        for (int i = 0; i < powerUpUI.panelName.Length; i++)
        {
            powerUpUI.panelUIs[i].SetPanelName(powerUpUI.panelName[i]);
        }
    }
    public void SetPanelData()
    {
        for (int i = 0; i < powerUpUI.panelUIs.Length; i++)
        {
            string accountText = "";
            List<int> pList = new List<int>();
            ItemGrade itemGrade = ResourceManager.Instance.XMLAccess.Randomgrade(); // ��� �̾Ƽ�
            if (i == 0)
            {
                for (int j = 0; j < ResourceManager.Instance.XMLAccess.powerUpPlayerList.Count; j++)
                {
                    if (ResourceManager.Instance.XMLAccess.powerUpPlayerList[j].grade == itemGrade.grade) // ���� ����ΰ͸� �ְ�
                    {
                        pList.Add(j);
                    }
                }
                int num = Random.Range(0, pList.Count); //�� ��� ���� �ɷ�ġ�� �ƹ��ų�
                num = pList[num];
                PowerUpPlayer p = ResourceManager.Instance.XMLAccess.powerUpPlayerList[num]; //����Ʈ�߿� �ϳ� ����
                accountText = $"{p.statName}��{p.powerUpSize}��ŭ ��ȭ�Ѵ�";

            }
            else if (i == 1)
            {
                for (int j = 0; j < ResourceManager.Instance.XMLAccess.powerUpItemList.Count; j++)
                {
                    if (ResourceManager.Instance.XMLAccess.powerUpItemList[j].grade == itemGrade.grade) // ���� ����ΰ͸� �ְ�
                    {
                        pList.Add(j);
                    }
                }
                int num = Random.Range(0, pList.Count); //�� ��� ���� �ɷ�ġ�� �ƹ��ų�
                num = pList[num];
                PowerUpItem p = ResourceManager.Instance.XMLAccess.powerUpItemList[num]; //����Ʈ�߿� �ϳ� ����
                accountText = $"{p.itemName}��{p.powerUpSize}��ŭ ��ȭ�Ѵ�";

            }
            else if (i == 2)
            {
                for (int j = 0; j < ResourceManager.Instance.XMLAccess.powerUpSkillList.Count; j++)
                {
                    if (ResourceManager.Instance.XMLAccess.powerUpSkillList[j].grade == itemGrade.grade) // ���� ����ΰ͸� �ְ�
                    {
                        pList.Add(j);
                    }
                }
                int num = Random.Range(0, pList.Count); //�� ��� ���� �ɷ�ġ�� �ƹ��ų�
                num = pList[num];
                PowerUpSkill p = ResourceManager.Instance.XMLAccess.powerUpSkillList[num]; //����Ʈ�߿� �ϳ� ����

                accountText = $"{p.skillName}��{p.powerUpSize}��ŭ ��ȭ�Ѵ�";
            }
            Debug.Log($"{i}, {itemGrade.color}, {accountText}, {itemGrade.money}");
            powerUpUI.SetPanelDataNoSprite(i, itemGrade.color, accountText, itemGrade.money);
        }
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
