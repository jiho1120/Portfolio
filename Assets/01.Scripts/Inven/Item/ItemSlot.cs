using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    public int count;
    public SOItem item;
    
    public Button removeButton;
    public Image icon;
    public Text countTxt;

    public void RemoveItem()
    {
        InventoryManager.Instance.Remove(this);

    }
    public void SetData(SOItem newItem)
    {
        item = newItem;
    }
    
}
