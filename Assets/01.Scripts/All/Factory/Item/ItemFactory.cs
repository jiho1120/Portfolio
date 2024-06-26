using System.Collections.Generic;
using UnityEngine;
using static AllEnum;

public class ItemFactory : Factory
{
    private DroppedItem newProduct;

    // �׸� ������ �ش� �׸� �������� �����ϴ� ����
    private Dictionary<string, ItemList> itemEnumMap;

    // �׸� ������ ���� �ʱ�ȭ�ϴ� ������
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
