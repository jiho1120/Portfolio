using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : Singleton<ItemManager>
{
    public ObjectPool<Item> itemPool { get; private set; }
    public Item itemPre; // 프리팹
    public Transform dropItemParent; // 하이어라키에 프리팹 생성 위치
    int ItemCount = 10;

    public void Init()
    {
        if (itemPool == null)
        {
            itemPool = new ObjectPool<Item>(itemPre, ItemCount, dropItemParent);
            itemPool.Init();
        }
    }
    public void AllItemDeActive()
    {
        for (int i = 0; i < itemPool.objList.Count; i++)
        {
            if (itemPool.objList[i] != null)
            {
                itemPool.ReturnObjectToPool(itemPool.objList[i]);
            }
        }
        
    }
    public void DropItem(int idx, Vector3 tr)
    {
        if (itemPre != null)
        {
            Item itemObject = itemPool.GetObjectFromPool();
            itemObject.SetItemData(idx);
            itemObject.transform.position = tr;
        }
    }


}
