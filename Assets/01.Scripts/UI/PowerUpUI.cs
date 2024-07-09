using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static AllEnum;
using Random = UnityEngine.Random;


public class PowerUpUI : MonoBehaviour
{
    public PanelUI[] panelUIs;
    public Transform PanelParent; //mainPanel
    public PanelUI PanelPrefab;
    public Button closeButton;
    int panelCount = 3;
    public string[] panelName = new string[3] { "�÷��̾� �ɷ�ġ", "������", "��ų" };

     void Awake() 
    {
        if (panelUIs.Length == 0)
        {
            SpwanPanel();
        }
        for (int i = 0; i < panelUIs.Length; i++)
        {
            panelUIs[i].Init();
            panelUIs[i].SetPanelTitle(panelName[i]);
            panelUIs[i].SetPanelType((PanelType)i);
        }
    }
    public void Active()
    {
        GameManager.Instance.VisibleCursor();
        gameObject.SetActive(true);
        SetPanelData();
    }
    public void DeActive()
    {
        GameManager.Instance.LockedCursor();
        gameObject.SetActive(false);
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

    public void SetPanelData()
    {
        Sprite img;
        int num;
        string name;
        float effect;
        List<int> pList;
        ItemGrade itemGrade;
        for (int i = 0; i < panelUIs.Length; i++)
        {
            pList = new List<int>();
            itemGrade = ResourceManager.Instance.XMLAccess.Randomgrade(); // ��� �̾Ƽ�
            if (i == 0)
            {
                for (int j = 0; j < ResourceManager.Instance.XMLAccess.powerUpPlayerList.Count; j++)
                {
                    if (ResourceManager.Instance.XMLAccess.powerUpPlayerList[j].grade == itemGrade.grade) // ���� ����ΰ͸� �ְ�
                    {
                        pList.Add(j);
                    }
                }
                //�� ��� ���� �ɷ�ġ�� �ƹ��ų�
                num = pList[Random.Range(0, pList.Count)];

                PowerUpPlayer p = ResourceManager.Instance.XMLAccess.powerUpPlayerList[num]; //����Ʈ�߿� �ϳ� ����

                img = ResourceManager.Instance.playerPowerUpIcon;
                name = p.statName;
                effect = p.powerUpSize;

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
                //�� ��� ���� �ɷ�ġ�� �ƹ��ų�
                num = pList[Random.Range(0, pList.Count)];

                PowerUpItem p = ResourceManager.Instance.XMLAccess.powerUpItemList[num]; //����Ʈ�߿� �ϳ� ����
                                                                                         // ���ڿ��� Enum�� �̸��� ��ġ�ϴ��� Ȯ��
                if (Enum.TryParse(p.itemName, true, out ItemList value))
                {
                    img = ResourceManager.Instance.GetSprite(DictName.ItemSpriteDict, value.ToString());
                }
                else
                {
                    return;
                }

                name = value.ToString();
                effect = p.powerUpSize;
            }
            else
            {
                for (int j = 0; j < ResourceManager.Instance.XMLAccess.powerUpSkillList.Count; j++)
                {
                    if (ResourceManager.Instance.XMLAccess.powerUpSkillList[j].grade == itemGrade.grade) // ���� ����ΰ͸� �ְ�
                    {
                        pList.Add(j);
                    }
                }
                num = pList[Random.Range(0, pList.Count)];
                PowerUpSkill p = ResourceManager.Instance.XMLAccess.powerUpSkillList[num]; //����Ʈ�߿� �ϳ� ����

                // ���ڿ��� Enum�� �̸��� ��ġ�ϴ��� Ȯ��
                if (Enum.TryParse(p.skillName, true, out SkillName value))
                {
                    img = ResourceManager.Instance.GetSprite(DictName.SkillSpriteDict, value.ToString());
                }
                else
                {
                    return;
                }
                name = value.ToString();
                effect = p.powerUpSize;
            }
            panelUIs[i].SetPanelData(img, itemGrade.color, name, effect, itemGrade.money);
        }

    }
    

}
