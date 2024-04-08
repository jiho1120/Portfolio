using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public ItemData itemData;
    [SerializeField] SpriteRenderer spriteRenderer;
    private void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }
    
    public void SetItemData(int idx)
    {
        itemData.index = idx;
        if (itemData.index <= 100)
        {
            for (int i = 0; i < DataManager.Instance.Equipment.Length; i++)
            {
                if (DataManager.Instance.Equipment[i].index == idx)
                {
                    itemData.SetItemData(DataManager.Instance.Equipment[i]);
                    
                }
            }
        }
        else
        {
            for (int i = 0; i < DataManager.Instance.Posion.Length; i++)
            {
                if (DataManager.Instance.Posion[i].index == idx)
                {
                    itemData.SetItemData(DataManager.Instance.Posion[i]);
                }
            }
        }
        spriteRenderer.sprite = itemData.icon;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            InvenManager.Instance.Additem(itemData);
                DataManager.Instance.SaveInvenInfo(DataManager.Instance.gameData.invenDatas);
            gameObject.SetActive(false);
        }
    }

}
