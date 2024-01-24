using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public SOItem itemData { get; private set; }
    public SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }
    
    public void SetItemData(SOItem data)
    {
        itemData = data;
        spriteRenderer.sprite = itemData.icon;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            bool isAdd = InventoryManager.Instance.checkAdd(itemData);
            if (isAdd)
            {
                InventoryManager.Instance.DataAdd(itemData);
                ItemManager.Instance.ReturnObjectToPool(this);
            }
            else
            {
                UiManager.Instance.OpenWarning("인벤토리 칸부족");

            }
        }
    }

}
