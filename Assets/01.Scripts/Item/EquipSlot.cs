using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static AllEnum;

public class EquipSlot : MonoBehaviour
{
    public ItemList itemlist;
    public Image icon;
    public TMP_Text lv;

    private void Awake()
    {
        icon = transform.GetChild(0).GetComponent<Image>();
        lv = GetComponentInChildren<TMP_Text>();
    }

    public void SetEquipSlotData()
    {
        DataManager.Instance.LoadInvenInfo();
        ItemData item = DataManager.Instance.gameData.invenDatas.EquipItemDatas[itemlist];

        if (item.level == 0) // 아이콘은 어차피 널임 레벨이 있으면 null이 아니란뜻
        {
            icon.sprite = ResourceManager.Instance.nullEquipSprite;
        }
        else
        {
            icon.sprite = ResourceManager.Instance.GetSprite(DictName.ItemSpriteDict,itemlist.ToString());
        }

        lv.text = $"LV : {item.level}";
    }

    public void ApplyItemStat()
    {
        DataManager.Instance.LoadInvenInfo();
        ItemData item = DataManager.Instance.gameData.invenDatas.EquipItemDatas[itemlist];
        switch (itemlist)
        {
            case AllEnum.ItemList.Head:
                break;
            case AllEnum.ItemList.Top:
                break;
            case AllEnum.ItemList.Gloves:
                break;
            case AllEnum.ItemList.Weapon:
                break;
            case AllEnum.ItemList.Belt:
                break;
            case AllEnum.ItemList.Bottom:
                break;
            case AllEnum.ItemList.Shoes:
                break;
            case AllEnum.ItemList.End:
                break;
        }
    }
}
