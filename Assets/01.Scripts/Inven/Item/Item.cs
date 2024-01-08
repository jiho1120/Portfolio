using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public int haveCount { get; private set; }
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
    public void AddCount(int val)
    {
        haveCount += val;
    }
    public void SetCount(int val)
    {
        haveCount = val;
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("충돌");
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("함수들어옴");
            InventoryManager.Instance.AddItem(this);

            this.gameObject.SetActive(false);
        } 
    }

}
