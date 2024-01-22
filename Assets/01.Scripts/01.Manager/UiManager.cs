using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;


public class UiManager : Singleton<UiManager>
{
    [SerializeField] private Camera cam;
    public PlayerConditionUI playerConditionUI;
    public Image fakeIcon;
    public GraphicRaycaster graphicRaycaster;
    public PowerUpUI powerUpUI;
    public EndPanel endPanel;
    public Text totalRound;
    public Text count;
    public GameObject wating;
    public Button stopTime;
    Text stopBtnText;
    public Button skipTime;
    public Text watingCount;
    public Text playerMoney;
    public Text goalCount;
    public GameObject note;
    public SpriteRenderer innerNote { get; private set; }
    public SpriteRenderer outterNote { get; private set; }


    public void Init()
    {
        SetCamera();
        stopBtnText = stopTime.transform.GetComponentInChildren<Text>();
        playerConditionUI.Init();
        powerUpUI.Init();
        SetPanelName();
        innerNote = note.transform.GetChild(0).GetComponent<SpriteRenderer>();
        outterNote = note.transform.GetChild(1).GetComponent<SpriteRenderer>();
        
    }
    public void SetUI()
    {
        if (GameManager.Instance.gameStart)
        {
            playerConditionUI.SetUI();
            count.text = string.Format("{0:N2}", GameManager.Instance.gameTime.ToString());
            totalRound.text = $"{GameManager.Instance.gameRound} - {GameManager.Instance.gameStage}";
            playerMoney.text = $"{GameManager.Instance.player.playerStat.money} G";
            goalCount.text = $" <color=#ff0000> {GameManager.Instance.killMonster}</color>" +
                $" / {GameManager.Instance.monsterGoal}";
        }
        else
        {
            if (GameManager.instance.isRunTime)
            {
                stopBtnText.text = "Ÿ�̸� ����";
            }
            else
            {
                stopBtnText.text = "Ÿ�̸� ����";
            }

            watingCount.text = Mathf.Ceil(GameManager.Instance.countTime).ToString();
            if (GameManager.Instance.countTime <= 1)
            {
                watingCount.text = "���� ����";
            }
        }
        
    }
    public void SetCamera()
    {
        if (cam == null)
            cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }
    public void StartShrike()
    {
        note.SetActive(true);
        outterNote.transform.localScale = new Vector3(3, 3, 3);
        innerNote.transform.localScale = new Vector3(1, 1, 1);
        StartCoroutine(ShrinkCircle());
    }
    IEnumerator ShrinkCircle()
    {
        while (true)
        {
            note.transform.position = GameManager.Instance.boss.transform.position + new Vector3(0, 3, 0);
            outterNote.transform.localScale -= new Vector3(0.01f, 0.01f, 0.01f);
            yield return new WaitForSeconds(0.01f);
            if (outterNote.transform.localScale == new Vector3(1, 1, 1))
            {
                note.SetActive(false);
                break;
            }
        }
    }
    public void ShowPowerUpPanel()
    {
        // UI ������ ����
        SetPanelData();
        // ���ų� ������ ��ư ������ ������
        PowerUpScreenOnOff();
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
            //Debug.Log($"{i}, {itemGrade.color}, {accountText}, {itemGrade.money}");
            powerUpUI.SetPanelUINoSprite(i, itemGrade.color, accountText, itemGrade.money);

        }
    }
    public void PowerUpScreenOnOff()
    {
        powerUpUI.ScreenOnOff();
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
    public void ActiveEndPanel()
    {
        endPanel.gameObject.SetActive(true);
        Time.timeScale = 0f;
        GameManager.Instance.LockCursor(false);
        endPanel.SetPanel();
    }
}
