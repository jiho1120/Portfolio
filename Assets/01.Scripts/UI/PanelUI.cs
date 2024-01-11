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
    private Button buyButton; // 여기에 가격세팅 결국 텍스트

    public void MatchingUI()
    {
        title = transform.GetChild(0).GetComponent<Text>();
        icon = transform.GetChild(1).GetComponent<Image>();
        account = transform.GetChild(2).transform.GetComponentInChildren<Text>();
        buyButton = transform.GetChild(3).GetComponent<Button>();
    }
    public void SetPanelName(string text)
    {
        title.text = text;
    }
    public void SetDate(Sprite image, string accountText, string money) // 어차피 위에 텍스트는 한번 정하고 안바뀜
    {
        icon.sprite = image;
        account.text = accountText;
        buyButton.GetComponentInChildren<Text>().text = money;
    }
}
