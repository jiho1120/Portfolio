using UnityEngine;
using UnityEngine.Pool;

public class DroppedItem : MonoBehaviour, IProduct
{
    public SOItem itemData;  // ScriptableObject 아이템 데이터
    [SerializeField] private string productName;
    public string ProductName { get => productName; set => productName = value; }

    #region 오브젝트 풀
    private IObjectPool<DroppedItem> objectPool;
    public IObjectPool<DroppedItem> ObjectPool { set => objectPool = value; }

    #endregion
    public void Init()
    {
        productName = itemData.itemList.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            InvenManager.Instance.AdditemToInven(itemData.ChangeSOItemToItemData());
            DataManager.Instance.SaveInvenInfo();

            objectPool.Release(this);

        }
    }
}
