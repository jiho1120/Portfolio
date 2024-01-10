using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class ItemSlot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public int slotIndex;
    public Button removeButton;
    public Image icon;
    public Text countTxt;
    //---------------슬롯으로써의 기능


    //해당 슬롯이 담은 아이템 정보~
    public int count;
    public SOItem item;

    public void SetSlotData(int _count, SOItem _item)
    {
        count = _count;
        item = _item;

       icon.sprite = item.icon;
        countTxt.text = count.ToString();
    }

    public void RemoveItem()
    {
        InventoryManager.Instance.Remove(this);

    }
    public void SetData(SOItem newItem)
    {
        item = newItem;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        icon.gameObject.SetActive(false);
        UiManager.Instance.fakeIcon.gameObject.SetActive(true);
        UiManager.Instance.fakeIcon.sprite = icon.sprite;

    }

    public void OnDrag(PointerEventData eventData)
    {
        UiManager.Instance.fakeIcon.transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        UiManager.Instance.fakeIcon.gameObject.SetActive(false);
        icon.gameObject.SetActive(true);

        icon.transform.localPosition = new Vector3(0, 5, 0);
        List<RaycastResult> rayList = new List<RaycastResult>();
        UiManager.Instance.graphicRaycaster.Raycast(eventData, rayList);
        ItemSlot nextSlot = null;
        for (int i = 0; i < rayList.Count; i++)
        {
            nextSlot = rayList[i].gameObject.GetComponent<ItemSlot>();
            if (nextSlot != null)
            {
                break;
            }
        }
        if (nextSlot != null)
        {
            InventoryManager.Instance.SwapItems(slotIndex, nextSlot.slotIndex);
        }
        else 
        {
            Debug.Log("����~");
        }
    }
    
    
}
