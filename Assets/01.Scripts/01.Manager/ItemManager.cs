using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using static AllEnum;



public class ItemManager : Singleton<ItemManager>
{
    public ItemFactory factory;

    [SerializeField] private IObjectPool<DroppedItem> objectPool;

    // 이미 풀에 있는 기존 항목을 반환하려고 하면 예외를 던집니다.
    [SerializeField] private bool collectionCheck = true;
    // 풀 용량 및 최대 크기를 제어하는 추가 옵션
    [SerializeField] private int defaultCapacity = 20;
    [SerializeField] private int maxSize = 100;
    
    Vector3 itemPos = new Vector3 (0, 0.5f, 0);

    
    private void Start()
    {
        objectPool = new ObjectPool<DroppedItem>
            (CreateItem,
               OnGetFromPool, OnReleaseToPool, OnDestroyPooledObject,
               collectionCheck, defaultCapacity, maxSize);
    }
    private DroppedItem CreateItem()
    {
        DroppedItem item;
        item = (DroppedItem)factory.GetProduct(ItemList.Head.ToString());
        item.ObjectPool = objectPool;
        return item;
    }

    private void OnGetFromPool(DroppedItem item)
    {
        item.gameObject.SetActive(true);
    }

    private void OnReleaseToPool(DroppedItem item)
    {
        item.gameObject.SetActive(false);
    }

    private void OnDestroyPooledObject(DroppedItem item)
    {
        Destroy(item.gameObject);
    }

    public DroppedItem DropRandomItem(Monster mon)
    {
        if (Random.value < 0.5f)
        {
            //돈 주기 
            mon.AddPlayerMoney();
            return null;
        }

        DroppedItem item;
        int itemEnumNum = Random.Range(0, (int)ItemList.End);
        ItemList itemtype = (ItemList)itemEnumNum;
        
        item = (DroppedItem)factory.GetProduct(itemtype.ToString());
        item.ObjectPool = objectPool;

        item.transform.position = mon.transform.position + itemPos;
        return item;

    }
}
