using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UiManager : Singleton<UiManager>
{
    public GameObject uiManager;
    public PlayerConditionUI playerConditionUI;
    public Image fakeIcon;
    public GraphicRaycaster graphicRaycaster;
    public PowerUpUI powerUpUI;
    public Text totalRound;
    public Text count;
    public GameObject wating;
    public Button stopTime;
    Text stopBtnText;
    public Button skipTime;
    public Text watingCount;
    public Text playerMoney;
    public Text goalCount;

    
    public void Init()
    {
        stopBtnText = stopTime.transform.GetComponentInChildren<Text>();
        playerConditionUI.Init();
        powerUpUI.Init();
        SetPanelName();
    }
    public void SetUI()
    {
        playerConditionUI.SetUI();
        totalRound.text = $"{GameManager.Instance.gameRound} - {GameManager.Instance.gameStage}";
        playerMoney.text = $"{GameManager.Instance.player.playerStat.money} G";
        goalCount.text = $" <color=#ff0000> {GameManager.Instance.killMonster}</color>" +
            $" / {GameManager.Instance.monsterGoal}";
        if (GameManager.instance.runTime)
        {
            stopBtnText.text = "Ÿ�̸� ����";
        }
        else
        {
            stopBtnText.text = "Ÿ�̸� ����";
        }

        count.text = string.Format("{0:N2}", GameManager.Instance.gameTime.ToString());
        watingCount.text = Mathf.Ceil(GameManager.Instance.countTime).ToString();
        if (GameManager.Instance.countTime <=  1)
        {
            watingCount.text = "���� ����";
        }
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
                powerUpUI.SetPanelData(i, "player", p.statName, p.powerUpSize, itemGrade.money);
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
                powerUpUI.SetPanelData(i,"item", p.itemName, p.powerUpSize, itemGrade.money);


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
                powerUpUI.SetPanelData(i,"skill", p.skillName, p.powerUpSize, itemGrade.money);
            }
            Debug.Log($"{i}, {itemGrade.color}, {accountText}, {itemGrade.money}");
            powerUpUI.SetPanelUINoSprite(i, itemGrade.color, accountText, itemGrade.money);

        }
    }

    public void ScreenOnOff(char key) // ���߿� �� �������� ����ϱ� ���ҰͰ��Ƽ� ����
    {
        switch (key)
        {
            case 'i':
                InventoryManager.Instance.InvenOnOff();
                break;
            default:
                break;
        }
    }

}
