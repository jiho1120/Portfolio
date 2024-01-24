using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class DragSlot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    int itemIndex = 0;
    public void OnBeginDrag(PointerEventData eventData)
    {
        GameObject obj = EventSystem.current.currentSelectedGameObject;
        ItemSlot slot = obj.GetComponent<ItemSlot>();
        OnItemClick(slot);
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {

        // ��ӵ� ������ �����ϸ� ������ ���� ��ȯ
        //SwapItems(itemIndex1,itemIndex2);
    }
    public void OnItemClick(ItemSlot clickedItem)
    {
        itemIndex = InventoryManager.Instance.itemList.IndexOf(clickedItem);

        if (itemIndex != -1)
        {
            // itemIndex�� ����Ͽ� ������ ����Ʈ���� �� ��°�� �ִ��� Ȯ���� �� ����

            Debug.Log("Ŭ���� �������� ������ ����Ʈ�� " + itemIndex + "��°�� �ֽ��ϴ�.");
        }
        else
        {
            Debug.Log("Ŭ���� �������� ������ ����Ʈ�� �����ϴ�.");
        }
    }
    //private void SwapItems(int slotA, int slotB)
    //{
    //    ItemSlot i = InventoryManager.Instance.itemList[slotB];
    //    InventoryManager.Instance.itemList[slotB] = InventoryManager.Instance.itemList[slotA];
    //    InventoryManager.Instance.itemList[slotA] = i;
        
    //}


}
