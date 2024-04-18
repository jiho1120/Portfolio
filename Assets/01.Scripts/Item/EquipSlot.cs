using TMPro;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class EquipSlot : MonoBehaviour
{
    public AllEnum.ItemList itemlist;
    public Image icon;
    public TMP_Text lv;

    private void Awake()
    {
        icon = transform.GetChild(0).GetComponent<Image>();
        lv = GetComponentInChildren<TMP_Text>();
    }
    public void SetEquipSlot()
    {
        int _lv;
        ItemData item = DataManager.Instance.gameData.invenDatas.EquipItemDatas[itemlist];

        if (item.icon == null)
        {
            icon.sprite = ResourceManager.Instance.nullEquipSprite;
        }
        else
        {
            icon.sprite = item.icon;
        }

        if (lv.text == null)
        {
            _lv = 0;
        }
        else
        {
            _lv = item.level;
        }
        lv.text = $"LV : {_lv}";
    }
    
}
