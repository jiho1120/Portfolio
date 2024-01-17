using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class PanelUI : MonoBehaviour
{
    public Sprite playerPowerUpIcon; // ��ȭâ���� �÷��̾� ��ȭ �������� �̰� �ϳ��� ������
    [SerializeField]
    private Text title;
    [SerializeField]
    private Image icon;
    [SerializeField]
    private Text account;
    [SerializeField]
    private Button buyButton;
    private Text moneyTxt;
    [SerializeField]
    private Image background;
    [SerializeField]
    private Color color;

    private string setType;
    private string powerUpName;
    private float effect;
    private int money;
    public void MatchingUI()
    {
        title = transform.GetChild(0).GetComponent<Text>();
        icon = transform.GetChild(1).GetComponent<Image>();
        account = transform.GetChild(2).transform.GetComponentInChildren<Text>();
        buyButton = transform.GetChild(3).GetComponent<Button>();
        moneyTxt = buyButton.GetComponentInChildren<Text>();
        background = GetComponent<Image>();
    }
    public void SetPanelName(string text)
    {
        title.text = text;
    }
    public void SetPanelDate(string setType, string powerUpName, float effect, int money) // ������ ���� �ؽ�Ʈ�� �ѹ� ���ϰ� �ȹٲ�
    {
        this.setType = setType;
        this.powerUpName = powerUpName;
        this.effect = effect;
        this.money = money;
    }
    public void SetPanelIcon(int num, string name)
    {
        if (num == 0)
        {
            icon.sprite = playerPowerUpIcon;
        }
        else if (num == 1)
        {
            for (int i = 0; i < InventoryManager.Instance.equipList.Length; i++)
            {                
                if (InventoryManager.Instance.equipList[i].itemType.ToString() == name)
                {
                    icon.sprite = InventoryManager.Instance.equipList[i].itemStat.icon;

                }
            }
        }
        else if (num == 2)
        {
            AllEnum.SkillName _name;
            bool parseSuccess = Enum.TryParse(name, out _name);

            if (parseSuccess)
            {
                if (SkillManager.Instance.skillDict.ContainsKey(_name))
                {
                    Skill value = SkillManager.Instance.skillDict[_name];
                    icon.sprite = value.skillStat.icon;
                }
            }
            else
            {
                Debug.LogError($"Enum �Ľ̿� �����߽��ϴ�. ��ȿ���� ���� SkillName: {powerUpName}");
            }
        }
    }
    public void SetPanelUINoSprite(string _color, string accountText, int _money) // ������ ���� �ؽ�Ʈ�� �ѹ� ���ϰ� �ȹٲ�
    {
        account.text = accountText;
        moneyTxt.text = _money.ToString();
        switch (_color)
        {
            case "Gray":
                background.color = Color.gray;
                return;
            case "Green":
                background.color = Color.green;
                return;
            case "Blue":
                background.color = Color.blue;
                return;
            case "Yellow":
                background.color = Color.yellow;
                return;
            default:
                background.color = Color.red;
                break;
        }
    }
    public void BuyButton()
    {
        int playerMoney = GameManager.Instance.player.playerStat.money;
        Debug.Log(playerMoney);
        if (playerMoney > money)
        {
            GameManager.Instance.player.playerStat.SetMoney(playerMoney - money);
            ApplyValue();
            UiManager.Instance.powerUpUI.ScreenOnOff();
        }
        else
        {
            Debug.Log("���� ������");
        }
    }
    public void ApplyValue()
    {
        if (setType == "player")
        {
            AllEnum.PlyerStat powerUpSkillName;
            bool parseSuccess = Enum.TryParse(powerUpName, out powerUpSkillName);
            if (parseSuccess)
            {
                GameManager.Instance.player.playerStat.AddAnything(powerUpSkillName, effect);
            }
            else
            {
                Debug.LogError($"Enum �Ľ̿� �����߽��ϴ�. ��ȿ���� ���� PlyerStat: {powerUpName}");
            }
        }
        else if (setType == "item")
        {
            for (int i = 0; i < InventoryManager.Instance.equipList.Length; i++)
            {
                if (InventoryManager.Instance.equipList[i].itemType.ToString() == powerUpName)
                {
                    InventoryManager.Instance.equipList[i].ApplyEffect(effect);
                }
            }
        }
        else if (setType == "skill")
        {
            AllEnum.SkillName powerUpSkillName;
            bool parseSuccess = Enum.TryParse(powerUpName, out powerUpSkillName);
            if (parseSuccess)
            {
                for (int i = 0; i < SkillManager.Instance.skillDict.Count; i++)
                {
                    if (SkillManager.Instance.skillDict.ContainsKey(powerUpSkillName))
                    {
                        Skill value = SkillManager.Instance.skillDict[powerUpSkillName];
                        value.skillStat.SetEffect(effect);
                    }
                }
            }
            else
            {
                Debug.LogError($"Enum �Ľ̿� �����߽��ϴ�. ��ȿ���� ���� SkillName: {powerUpName}");
            }
        }
        else
        {
            Debug.Log("���� ���ε�");
        }
    }
}
