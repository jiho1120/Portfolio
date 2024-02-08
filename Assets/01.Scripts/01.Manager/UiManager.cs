using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UiManager : Singleton<UiManager>, ReInitialize
{
    public GameObject startScene;
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
    public GameObject warning;
    Text warningText;
    public GameObject canvas;


    public SpriteRenderer innerNote { get; private set; }
    public SpriteRenderer outterNote { get; private set; }

    public void Initialize()
    {
        //ReStart();
        stopBtnText = stopTime.transform.GetComponentInChildren<Text>();
        playerConditionUI.Init();
        powerUpUI.Init();
        SetPanelName();
        innerNote = note.transform.GetChild(0).GetComponent<SpriteRenderer>();
        outterNote = note.transform.GetChild(1).GetComponent<SpriteRenderer>();
        warningText = warning.transform.GetChild(0).GetComponent<Text>();
    }

    public void ReStart() // Wating ������
    {
        startScene.gameObject.SetActive(false);
        playerConditionUI.gameObject.SetActive(true);
        totalRound.gameObject.SetActive(true);
        count.gameObject.SetActive(true);
        wating.gameObject.SetActive(true);
        stopTime.gameObject.SetActive(true);
        skipTime.gameObject.SetActive(true);
        watingCount.gameObject.SetActive(true);
        playerMoney.gameObject.SetActive(true);
        goalCount.gameObject.SetActive(true);
        note.gameObject.SetActive(false);
    }

    public void Deactivate()
    {

    }

    public void DontUse()
    {
    }
   
    public void SetGameUI()
    {
        playerConditionUI.SetUI();
        count.text = string.Format("{0:N2}", GameManager.Instance.gameTime.ToString());
            totalRound.text = $"{GameManager.Instance.gameRound} - {GameManager.Instance.gameStage}";
        playerMoney.text = $"{GameManager.Instance.player.playerStat.money} G";
        goalCount.text = $" <color=#ff0000> {GameManager.Instance.killMonster}</color>" +
            $" / {GameManager.Instance.monsterGoal}";
    }
    public void SetStopTimer()
    {
        if (GameManager.instance.isRunTime)
        {
            stopBtnText.text = "Ÿ�̸� ����";
        }
        else
        {
            stopBtnText.text = "Ÿ�̸� ����";
        }
    }
    public void SetWaitingUI()
    {
        watingCount.text = GameManager.Instance.countTime.ToString();
        if (GameManager.Instance.countTime <= 1)
        {
            watingCount.text = "���� ����";
        }
    }

    public void StartShrinke()
    {
        note.SetActive(true);
        outterNote.transform.localScale = new Vector3(3, 3, 3);
        innerNote.transform.localScale = new Vector3(1, 1, 1);
        StartCoroutine(ShrinkCircle());
    }
    public void StopShrinke()
    {
        outterNote.transform.localScale = new Vector3(3, 3, 3);
        innerNote.transform.localScale = new Vector3(1, 1, 1);
        note.SetActive(false);
        StopCoroutine(ShrinkCircle());
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
                powerUpUI.SetPanelData(i, "item", p.itemName, p.powerUpSize, itemGrade.money);

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
                powerUpUI.SetPanelData(i, "skill", p.skillName, p.powerUpSize, itemGrade.money);
            }
            
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
        StartCoroutine(EndPanel());
    }
    IEnumerator EndPanel()
    {
        ItemManager.Instance.ReturnAllObjectToPool();
        MonsterManager.Instance.StopSpawnMonster();
        MonsterManager.Instance.CleanMonster();//=>�� ����ִ� �ֵ鸸 ����. (�ܼ��� ����. 
        yield return new WaitForSeconds(1f);
        endPanel.gameObject.SetActive(true);
        endPanel.SetPanel();
        Time.timeScale = 0f;
        GameManager.Instance.StopNum++;
        GameManager.Instance.LockCursor(false);
    } 
    public void OpenWarning(string _text)
    {
        GameManager.Instance.StopNum++;
        Time.timeScale = 0f;
        GameManager.Instance.LockCursor(false);
        warning.SetActive(true);
        warningText.text = _text;
    }
    public void CloseWarning()
    {
        GameManager.Instance.StopNum--;
        GameManager.Instance.LockCursor(true);
        warning.SetActive(false);
        if (GameManager.Instance.StopNum == 0)
        {
            Time.timeScale = 1f;
        }
    }

}
