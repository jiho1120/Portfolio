using System.Collections.Generic;
using UnityEngine;
using static AllEnum;

public class ItemFactory : Factory
{
    [SerializeField]private Transform Tr;
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
            return CreateDropItem(itemEnum);
        }

        Debug.LogError($"Item type {type} not recognized.");
        return null;
    }

    private IProduct CreateDropItem(ItemList itemEnum)
    {
        obj = Instantiate(ResourceManager.Instance.GetPrefab(DictName.ItemDict, itemEnum.ToString()), Tr);
        newProduct = obj.GetComponent<DroppedItem>();
        newProduct.Init();
        return newProduct;
    }
}
