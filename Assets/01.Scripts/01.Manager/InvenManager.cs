using UnityEngine;
using static AllEnum;
using static UnityEditor.Progress;

public class InvenManager : Singleton<InvenManager>
{
    public ItemData GetInvenItemData(ItemData itemData)
    {
        ItemData _itemdata = DataManager.Instance.gameData.invenDatas.invenItemDatas.Find(x => x.index == itemData.index && x.level == itemData.level);

        return _itemdata;
    }

    public ItemData GetPosionInvenItemData(ItemData itemData)
    {
        ItemData _itemdata = DataManager.Instance.gameData.invenDatas.PosionItemDatas[itemData.itemList];

        return _itemdata;
    }

    public void AdditemToInven(ItemData itemData)
    {
        if (itemData.index == -1)
        {
            return;
        }
        ItemData _itemData = GetInvenItemData(itemData);

        if (itemData == null)
        {
            Debug.Log(itemData + "널");
        }
        else if (_itemData == null)
        {
            DataManager.Instance.gameData.invenDatas.invenItemDatas.Add(itemData);
        }
        else
        {
            if (_itemData.count < 99)
            {
                _itemData.count++;
            }
            else
            {
                DataManager.Instance.gameData.invenDatas.invenItemDatas.Add(itemData);
            }
        }
    }
    public void AddToPosionInven(ItemData itemData)
    {
        Debug.Log("더하기 들어와서 인덱스 : " + itemData.index);
        if (itemData.index == -1)
        {
            return;
        }
        ItemData _itemData = GetPosionInvenItemData(itemData);
        if (_itemData.count >= 99)
        {
            return; // 99 이상 못넣음
        }
        else if (_itemData.index == -1) // 기본값이 있어서 null일수가 없음
        {
            _itemData.SetItemData(itemData);
            DataManager.Instance.gameData.invenDatas.invenItemDatas.Remove(itemData);
        }
        else if (_itemData.count < 99)// 양쪽에 아이템이있으면
        {
            _itemData.count += itemData.count;
            if (_itemData.count > 99)
            {
                itemData.count = _itemData.count - 99;
                _itemData.count = 99;
                // 나머지는 인벤에 99개 아래인 같은 포션과 합쳐지게
                AdditemToInven(itemData);
            }
            else if (_itemData.count <= 99)
            {
                DataManager.Instance.gameData.invenDatas.invenItemDatas.Remove(itemData);
            }
        }
        DataManager.Instance.SaveInvenInfo();
    }


    public void AddEquipItemToEquipDatas(ItemList itemList, ItemData itemData)
    {
        DataManager.Instance.gameData.invenDatas.EquipItemDatas[itemList] = itemData;
    }

    public void SetPlayerStatWithEquipItem()
    {

    }

}
