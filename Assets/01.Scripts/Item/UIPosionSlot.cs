using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class UIPosionSlot : MonoBehaviour
{
    public AllEnum.ItemList ItemList;
    public Image imgIcon;
    public TMP_Text txtAmount;
    public string useSlotKey { get; private set; } //해당키 누르면 아이템 사용 // 나중에 123이 아니라 a나 ㅁ같은 키로 사용가능하게 만들기
    int index;
    public void Init(ItemData item)
    {
        index = item.index;

        if (item.count < 1)
        {
            imgIcon.sprite = ResourceManager.Instance.nullEquipSprite;
        }
        else if (item.count >= 1)
        {
            for (int i = 0; i < DataManager.Instance.soItem.Length; i++)
            {

                if (index == DataManager.Instance.soItem[i].index)
                {
                    imgIcon.sprite = DataManager.Instance.soItem[i].icon;
                    break;
                }
            }
            //imgIcon.SetNativeSize();
        }

        txtAmount.text = item.count.ToString();
        txtAmount.gameObject.SetActive(item.count > 0);
    }

    public void SetUseSlotChar(string key)
    {
        useSlotKey = key;
    }
    public void UsePosion()
    {
        ItemData item = DataManager.Instance.gameData.invenDatas.PosionItemDatas[ItemList];
        if (item.count < 1)
        {
            return;
        }
        switch (ItemList)
        {
            case AllEnum.ItemList.HpPotion:
                GameManager.Instance.player.SetHp(GameManager.Instance.player.Stat.hp + item.hp);
                break;
            case AllEnum.ItemList.MpPotion:
                GameManager.Instance.player.SetMp(GameManager.Instance.player.Stat.hp + item.mp);
                break;
            case AllEnum.ItemList.UltimatePotion:
                GameManager.Instance.player.SetUltimate(item.ultimateGauge);
                break;

            default:
                break;
        }
        --item.count;
        Init(item);
        UIManager.Instance.SetPlayerUI();
    }
}
