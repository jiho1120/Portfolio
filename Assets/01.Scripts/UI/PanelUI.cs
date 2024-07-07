using UnityEngine;
using UnityEngine.UI;
using static AllEnum;


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

    // ������
    Panel panelData;
    
    public void Init()
    {
        title = transform.GetChild(0).GetComponent<Text>();
        icon = transform.GetChild(1).GetComponent<Image>();
        account = transform.GetChild(2).transform.GetComponentInChildren<Text>();
        buyButton = transform.GetChild(3).GetComponent<Button>();
        moneyTxt = buyButton.GetComponentInChildren<Text>();
        background = GetComponent<Image>();
        panelData = GetComponent<Panel>();

    }
    public void SetPanelTitle(string text)
    {
        title.text = text;
    }
    public void SetPanelType(PanelType type)
    {
        panelData.SetPanelType(type);
    }
    public void SetPanelData(Sprite img, string _color, string name,float effect, int money) // ������ ���� �ؽ�Ʈ�� �ѹ� ���ϰ� �ȹٲ�
    {
        icon.sprite = img;
        SetPanelColor(_color);
        account.text = $"{name}��{effect}��ŭ ��ȭ�Ѵ�";
        moneyTxt.text = money.ToString();
        panelData.SetSelldata(name,effect,money);
    }
    
    public void SetPanelColor(string _color)
    {
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
        int playerMoney = GameManager.Instance.player.Stat.money;
        if (playerMoney >= panelData.SellMoney)
        {
            GameManager.Instance.player.AddMoney(-panelData.SellMoney); // ���̳ʽ��� ����
            panelData.ApplyValue();
        }
        else
        {
            //UIManager.Instance.OpenWarning("���� ������");
            Debug.Log("���� ������");
        }
        UIManager.Instance.powerUpUI.DeActive();

    }
    
}
