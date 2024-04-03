using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvenManager : Singleton<InvenManager>
{
    // inven.Additem(item); 아이템이 있으면 더하기 없으면 추가
    // 넣고 인포데이터를 저장
    public void Additem(SOItem soItem)
    {
        int id = soItem.index;
        var FoundInfo = DataManager.Instance.gameData.invenDatas.itemDatas.Find(x => x.index == id);
        if (FoundInfo == null)
        {
            ItemData info = new ItemData();
            info.SetItemData(soItem);
            DataManager.Instance.gameData.invenDatas.itemDatas.Add(info);

        } else
        {
            FoundInfo.count++;
        }
    }

}
