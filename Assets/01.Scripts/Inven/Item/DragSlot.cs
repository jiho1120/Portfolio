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

        // 드롭된 슬롯이 존재하면 아이템 정보 교환
        //SwapItems(itemIndex1,itemIndex2);
    }
    public void OnItemClick(ItemSlot clickedItem)
    {
        itemIndex = InventoryManager.Instance.itemList.IndexOf(clickedItem);

        if (itemIndex != -1)
        {
            // itemIndex를 사용하여 아이템 리스트에서 몇 번째에 있는지 확인할 수 있음

            Debug.Log("클릭된 아이템은 아이템 리스트의 " + itemIndex + "번째에 있습니다.");
        }
        else
        {
            Debug.Log("클릭된 아이템은 아이템 리스트에 없습니다.");
        }
    }
    //private void SwapItems(int slotA, int slotB)
    //{
    //    ItemSlot i = InventoryManager.Instance.itemList[slotB];
    //    InventoryManager.Instance.itemList[slotB] = InventoryManager.Instance.itemList[slotA];
    //    InventoryManager.Instance.itemList[slotA] = i;
        
    //}


}
