using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using static AllEnum;



public class ItemManager : Singleton<ItemManager>
{
    public ItemFactory factory;

    // ������ Ÿ�Ժ� ������Ʈ Ǯ
    private Dictionary<ItemList, IObjectPool<DroppedItem>> itemPools = new Dictionary<ItemList, IObjectPool<DroppedItem>>();

    // �̹� Ǯ�� �ִ� ���� �׸��� ��ȯ�Ϸ��� �ϸ� ���ܸ� �����ϴ�.
    [SerializeField] private bool collectionCheck = true;
    // Ǯ �뷮 �� �ִ� ũ�⸦ �����ϴ� �߰� �ɼ�
    [SerializeField] private int defaultCapacity = 20;
    [SerializeField] private int maxSize = 100;

    Vector3 itemPos = new Vector3(0, 0.5f, 0);

    private void Start()
    {
        foreach (ItemList itemType in System.Enum.GetValues(typeof(ItemList)))
        {
            if (itemType == ItemList.End) continue; // End�� ������ Ÿ���� �ƴϹǷ� ����

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
            // �� �ֱ�
            mon.AddPlayerMoney();
            return;
        }

        // �������� ������ Ÿ�� ����
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
    //public ItemFactory factory;

    //[SerializeField] private IObjectPool<DroppedItem> objectPool;

    //// �̹� Ǯ�� �ִ� ���� �׸��� ��ȯ�Ϸ��� �ϸ� ���ܸ� �����ϴ�.
    //[SerializeField] private bool collectionCheck = true;
    //// Ǯ �뷮 �� �ִ� ũ�⸦ �����ϴ� �߰� �ɼ�
    //[SerializeField] private int defaultCapacity = 20;
    //[SerializeField] private int maxSize = 100;

    //Vector3 itemPos = new Vector3 (0, 0.5f, 0);


    //private void Start()
    //{
    //    objectPool = new ObjectPool<DroppedItem>
    //        (CreateItem,
    //           OnGetFromPool, OnReleaseToPool, OnDestroyPooledObject,
    //           collectionCheck, defaultCapacity, maxSize);
    //}
    //private DroppedItem CreateItem()
    //{
    //    DroppedItem item;
    //    int num = Random.Range(0, (int)ItemList.End);
    //    ItemList itemList = (ItemList)num;

    //    item = (DroppedItem)factory.GetProduct(itemList.ToString());
    //    item.ObjectPool = objectPool;
    //    return item;
    //}
    //public DroppedItem DropRandomItem(Monster mon)
    //{
    //    if (Random.value < 0.5f)
    //    {
    //        //�� �ֱ� 
    //        mon.AddPlayerMoney();
    //        return null;
    //    }

    //    DroppedItem item = GetItem();

    //    item.transform.position = mon.transform.position + itemPos;
    //    return item;

    //}
    //public DroppedItem GetItem()
    //{
    //    DroppedItem item = objectPool.Get();

    //    return item;
    //}

    //private void OnGetFromPool(DroppedItem item)
    //{
    //    item.gameObject.SetActive(true);
    //}

    //private void OnReleaseToPool(DroppedItem item)
    //{
    //    item.gameObject.SetActive(false);
    //}

    //private void OnDestroyPooledObject(DroppedItem item)
    //{
    //    Destroy(item.gameObject);
    //}


}
