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
    [SerializeField]
    private Image background;
    [SerializeField]
    private Color color;

    public void MatchingUI()
    {
        title = transform.GetChild(0).GetComponent<Text>();
        icon = transform.GetChild(1).GetComponent<Image>();
        account = transform.GetChild(2).transform.GetComponentInChildren<Text>();
        buyButton = transform.GetChild(3).GetComponent<Button>();
        background = GetComponent<Image>();
    }
    public void SetPanelName(string text)
    {
        title.text = text;
    }
    public void SetDate(string _color, Sprite image, string accountText, int money) // 어차피 위에 텍스트는 한번 정하고 안바뀜
    {
        icon.sprite = image;
        account.text = accountText;
        buyButton.GetComponentInChildren<Text>().text = money.ToString();
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
    public void SetDataWithoutSprite(string _color, string accountText, int money) // 어차피 위에 텍스트는 한번 정하고 안바뀜
    {
        account.text = accountText;
        buyButton.GetComponentInChildren<Text>().text = money.ToString();
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
    public void SelectButton()
    {

    }
}
