using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PanelUI : MonoBehaviour
{
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
    public void SetPanelDate(string setType, string powerUpName, float effect, int money) // 어차피 위에 텍스트는 한번 정하고 안바뀜
    {
        this.setType = setType;
        this.powerUpName = powerUpName;
        this.effect = effect;
        this.money = money;
    }
    public void SetPanelUI(string _color, Sprite image, string accountText, int _money) // 어차피 위에 텍스트는 한번 정하고 안바뀜
    {
        icon.sprite = image;
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
    public void SetPanelUINoSprite(string _color, string accountText, int _money) // 어차피 위에 텍스트는 한번 정하고 안바뀜
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
        if (playerMoney > money)
        {
            GameManager.Instance.player.playerStat.SetMoney(playerMoney -= money);
            ApplyValue();
        }
        else
        {
            Debug.Log("돈이 부족함");
        }
    }
    public void ApplyValue()
    {
        if (setType == "player")
        {
            GameManager.Instance.player.playerStat.AddAnything(powerUpName, effect);
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

        }
        else
        {
            Debug.Log("없는 것인데");
        }
    }
}
