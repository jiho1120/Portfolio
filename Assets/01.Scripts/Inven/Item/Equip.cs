using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Equip : MonoBehaviour
{
    public AllEnum.ItemType itemType;
    public Text level;
    public SOItem soItem;
    public ItemStat itemStat { get; private set; }

    public void Init()
    {
        itemStat = new ItemStat(soItem);
        level = transform.Find("ItemLevel").GetComponent<Text>();
        level.text = "Lv : " + itemStat.level;
    }
    
    public void LevelUp()
    {
        itemStat.AddLevel(1);
        level.text = "Lv : " + itemStat.level;
    }

    public void ApplyEffect(float effect)
    {
        switch (itemType)
        {
            case AllEnum.ItemType.Head:
                itemStat.AddStat(AllEnum.ItemType.Head, effect);
                break;
            case AllEnum.ItemType.Top:
                itemStat.AddStat(AllEnum.ItemType.Top, effect);

                break;
            case AllEnum.ItemType.Gloves:
                itemStat.AddStat(AllEnum.ItemType.Gloves, effect);

                break;
            case AllEnum.ItemType.Weapon:
                itemStat.AddStat(AllEnum.ItemType.Weapon, effect);

                break;
            case AllEnum.ItemType.Belt:
                itemStat.AddStat(AllEnum.ItemType.Belt, effect);

                break;
            case AllEnum.ItemType.Bottom:
                itemStat.AddStat(AllEnum.ItemType.Bottom, effect);
                break;
            case AllEnum.ItemType.Shoes:
                itemStat.AddStat(AllEnum.ItemType.Shoes, effect);
                break;
            default:
                Debug.Log("¾øÀ½");
                break;
        }
        

    }

}
