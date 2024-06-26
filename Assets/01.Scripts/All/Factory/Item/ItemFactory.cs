using System.Collections.Generic;
using UnityEngine;
using static AllEnum;

public class ItemFactory : Factory
{
    private DroppedItem newProduct;

    // 항목 유형을 해당 항목 열거형에 매핑하는 사전
    private Dictionary<string, ItemList> itemEnumMap;

    // 항목 열거형 맵을 초기화하는 생성자
    void Awake()
    {
        itemEnumMap = new Dictionary<string, ItemList>()
        {
            { "HpPotion", ItemList.HpPotion },
            { "MpPotion", ItemList.MpPotion },
            { "UltimatePotion", ItemList.UltimatePotion },
            { "Head", ItemList.Head },
            { "Top", ItemList.Top },
            { "Gloves", ItemList.Gloves },
            { "Weapon", ItemList.Weapon },
            { "Belt", ItemList.Belt },
            { "Bottom", ItemList.Bottom },
            { "Shoes", ItemList.Shoes }
        };
    }

    public override IProduct GetProduct(string type)
    {
        if (itemEnumMap.TryGetValue(type, out var itemEnum))
        {
            return CreateEquipItem(itemEnum);
        }

        Debug.LogError($"Item type {type} not recognized.");
        return null;
    }

    private IProduct CreateEquipItem(ItemList itemEnum)
    {
        obj = Instantiate(ResourceManager.Instance.GetPrefab(DictName.ItemDict, itemEnum.ToString()));
        newProduct = obj.GetComponent<DroppedItem>();
        newProduct.Init();
        return newProduct;
    }
}
