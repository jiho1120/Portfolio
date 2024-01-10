using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class ItemManager : Singleton<ItemManager>
{
    public Queue<Item> itemPool = new Queue<Item>();
    public Item itemDrop;
    int maxIDroptemLength = 20;
    public void Init()
    {
        for (int i = 0; i < maxIDroptemLength; i++)
        {
            Item obj = Instantiate(itemDrop);
            obj.gameObject.SetActive(false);
            itemPool.Enqueue(obj);
        }
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

        Item itemObject = itemPool.Dequeue();
        itemObject.gameObject.SetActive(true);

        return itemObject;
    }
}
