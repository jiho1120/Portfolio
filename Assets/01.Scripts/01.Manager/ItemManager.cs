using System.Collections.Generic;
using UnityEngine;



public class ItemManager : Singleton<ItemManager>
{
    public Transform dropItemParent;
    public Queue<Item> itemPool = new Queue<Item>();
    public List<Item> infoList = new List<Item>();
    public Item itemDrop;
    int maxDropItemLength = 20;
    public void Init()
    {
        for (int i = 0; i < maxDropItemLength; i++)
        {
            MakeItem();
        }
    }
    
    public void MakeItem()
    {
        Item obj = Instantiate(itemDrop, dropItemParent);
        obj.gameObject.SetActive(false);
        infoList.Add(obj);
        itemPool.Enqueue(obj);
    }
    public void DropItem(int val, Vector3 tr)
    {
        if (itemDrop != null)
        {
            Item itemObject = GetObjectFromPool();

            for (int i = 0; i < ResourceManager.Instance.itemDataAll.Length; i++)
            {
                int index = ResourceManager.Instance.itemDataAll[i].index;
                if (index == val)
                {
                    itemObject.SetItemData(ResourceManager.Instance.itemDataAll[i]);
                }
            }
            itemObject.transform.position = tr;
        }
    }

    public Item GetObjectFromPool()
    {
        if (itemPool.Count == 0)
        {
            MakeItem();
        }
        Item itemObject = itemPool.Dequeue();
        itemObject.gameObject.SetActive(true);

        return itemObject;
    }
    public void ReturnObjectToPool(Item tInfo)
    {
        tInfo.gameObject.SetActive(false);
        itemPool.Enqueue(tInfo);
    }
    public void ReturnAllObjectToPool()
    {
        for (int i = 0; i < infoList.Count; i++)
        {
            if (infoList[i] != null)
            {
                ReturnObjectToPool(infoList[i]);
            }
        }
       
    }
}
