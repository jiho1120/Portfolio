using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvenManager : Singleton<InvenManager>
{
    // inven.Additem(item); 아이템이 있으면 더하기 없으면 추가
    // 넣고 인포데이터를 저장
    //public void Additem(ItemData itemData)
    //{
    //    int id = itemData.index;
    //    var FoundInfo = DataManager.Instance.gameData.invenDatas.invenItemDatas.Find(x => x.index == id);
    //    ItemData info = new ItemData();
    //    info.SetItemData(itemData);
    //    if (FoundInfo == null)
    //    {
    //        DataManager.Instance.gameData.invenDatas.invenItemDatas.Add(info);

    //    } else
    //    {
    //        if (FoundInfo.count < 99)
    //        {
    //            FoundInfo.count++;
    //        }
    //        else
    //        {
    //            DataManager.Instance.gameData.invenDatas.invenItemDatas.Add(info);
    //        }
    //    }
    //}
    public void Additem(ItemData itemData)
    {
        ItemData _itemdata = DataManager.Instance.gameData.invenDatas.invenItemDatas.Find(x => x.index == itemData.index && x.level == itemData.level);

        if (itemData == null)
        {
            Debug.Log(itemData + "널");
        }
        else
        {
            if (_itemdata == null)
            {
                DataManager.Instance.gameData.invenDatas.invenItemDatas.Add(itemData);
            }
            else
            {
                if (_itemdata.count < 99)
                {
                    _itemdata.count++;
                    Debug.Log(_itemdata.count);

                }
                else
                {
                    DataManager.Instance.gameData.invenDatas.invenItemDatas.Add(itemData);
                }
            }
        }
    }
}
