using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvenManager : Singleton<InvenManager>
{
    public ItemData GetInvenItemData(ItemData itemData)
    {
        ItemData _itemdata = DataManager.Instance.gameData.invenDatas.invenItemDatas.Find(x => x.index == itemData.index && x.level == itemData.level);

        return _itemdata;
    }
    //public void Additem(ItemData itemData)
    //{
    //    ItemData _itemdata = GetInvenItemData(itemData);

    //    if (itemData == null)
    //    {
    //        Debug.Log(itemData + "³Î");
    //    }
    //    else
    //    {
    //        if (_itemdata == null)
    //        {
    //            DataManager.Instance.gameData.invenDatas.invenItemDatas.Add(itemData);
    //        }
    //        else
    //        {
    //            if (_itemdata.count < 99)
    //            {
    //                _itemdata.count++;
    //                Debug.Log(_itemdata.count);

    //            }
    //            else
    //            {
    //                DataManager.Instance.gameData.invenDatas.invenItemDatas.Add(itemData);
    //            }
    //        }
    //    }
    //}
    public void Additem(ItemData itemData)
    {
        if (itemData.index == -1)
        {
            return;
        }
        ItemData _itemData = GetInvenItemData(itemData);

        if (itemData == null)
        {
            Debug.Log(itemData + "³Î");
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
                Debug.Log(_itemData.count);

            }
            else
            {
                DataManager.Instance.gameData.invenDatas.invenItemDatas.Add(itemData);
            }
        }
    }
}
