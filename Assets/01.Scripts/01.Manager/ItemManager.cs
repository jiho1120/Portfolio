using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using static AllEnum;



public class ItemManager : Singleton<ItemManager>
{
    public ItemFactory factory;

    // 아이템 타입별 오브젝트 풀
    private Dictionary<ItemList, IObjectPool<DroppedItem>> itemPools = new Dictionary<ItemList, IObjectPool<DroppedItem>>();

    // 이미 풀에 있는 기존 항목을 반환하려고 하면 예외를 던집니다.
    [SerializeField] private bool collectionCheck = true;
    // 풀 용량 및 최대 크기를 제어하는 추가 옵션
    [SerializeField] private int defaultCapacity = 20;
    [SerializeField] private int maxSize = 100;

    Vector3 itemPos = new Vector3(0, 0.5f, 0);

    private void Start()
    {
        foreach (ItemList itemType in System.Enum.GetValues(typeof(ItemList)))
        {
            if (itemType == ItemList.End) continue; // End는 아이템 타입이 아니므로 제외

            itemPools[itemType] = new ObjectPool<DroppedItem>(
                () => CreateItem(itemType),
                OnGetFromPool, OnReleaseToPool, OnDestroyPooledObject,
                collectionCheck, defaultCapacity, maxSize);
        }
    }

    private DroppedItem CreateItem(ItemList itemType)
    {
        DroppedItem item = (DroppedItem)factory.GetProduct(itemType.ToString());
        item.ObjectPool = itemPools[itemType];
        return item;
    }

    public void DropRandomItem(Monster mon)
    {
        if (Random.value < 0.5f)
        {
            // 돈 주기
            GameManager.Instance.player.AddMoney(mon.Stat.money);
            return;
        }

        // 랜덤으로 아이템 타입 선택
        int num = Random.Range(0, (int)ItemList.End);
        ItemList itemType = (ItemList)num;

        DroppedItem item = GetItem(itemType);
        item.transform.position = mon.transform.position + itemPos;
    }

    public DroppedItem GetItem(ItemList itemType)
    {
        return itemPools[itemType].Get();
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
    public void RecallAllItems()
    {
        // 현재 활성화된 모든 DroppedItem 오브젝트를 찾습니다.
        DroppedItem[] activeItems = FindObjectsOfType<DroppedItem>();

        foreach (var item in activeItems)
        {
            // 각 아이템을 해당 오브젝트 풀로 반환합니다.
            if (itemPools.TryGetValue(item.itemData.itemList, out var pool))
            {
                pool.Release(item);
            }
        }
    }
}
