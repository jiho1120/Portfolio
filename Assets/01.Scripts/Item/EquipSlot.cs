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
        int itemLevel = DataManager.Instance.gameData.invenDatas.EquipItemDatas[itemlist].level;

        if (itemLevel == 0) // �������� ������ ���� ������ ������ null�� �ƴ϶���
        {
            icon.sprite = ResourceManager.Instance.nullEquipSprite;
        }
        else
        {
            icon.sprite = ResourceManager.Instance.GetSprite(DictName.ItemSpriteDict,itemlist.ToString());
        }

        lv.text = $"LV : {itemLevel}";
    }

}
