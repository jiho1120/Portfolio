using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class InventoryManager : Singleton<InventoryManager>
{
    public List<ItemSlot> itemList = new List<ItemSlot>();
    int maxItemListLenght = 10;
    int maxItemLenght = 2;
    public Equip[] equipList;

    public Transform ItemContent;
    public GameObject InventoryItem;
    public Toggle EnableRemove;

    public GameObject inven;
    public bool invenOn = false;
    void Start()

    {
        SetItemListCount();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void SetItemListCount()
    {
        for (int i = 0; i < maxItemListLenght; i++)
        {
            ItemSlot obj = Instantiate(InventoryItem, ItemContent).GetComponent<ItemSlot>();
            itemList.Add(obj);
        }
    }
    public void InvenOnOff()
    {
        if (invenOn)
        {
            Time.timeScale = 0f; // 시간의 흐름이 멈춤  //코루틴 안되고, 업데이트 안 되고 , 픽스드 가능, 드래그도 가능
            inven.SetActive(true);
            ListItems();
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Time.timeScale = 1f;
            inven.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
    public bool checkAdd(SOItem item)
    {
        for (int i = 0; i < itemList.Count; i++)
        {
            if(itemList[i].item.index == -1)
            {
                return true;
            }
            else if (item.index > 100)
            {
                if (itemList[i].item.index == item.index)
                {
                    int remainingQuantity = maxItemLenght - itemList[i].count;
                    if (remainingQuantity > 0)
                    {
                        if (item.count <= remainingQuantity)
                        {
                            return true;
                        }
                        else
                        {
                            itemList[i].count += remainingQuantity;
                            item.count -= remainingQuantity;
                        }
                    }
                }
            }
        }
        if (item.count > 0)
        {
            return false;
        }
        else
        {
            return true;
        }

    }
    public void DataAdd(SOItem item)
    {
        for (int i = 0; i < itemList.Count; i++)
        {
            if (itemList[i].item.index == -1)
            {
                itemList[i].count = item.count;
                itemList[i].SetData(item);
                return;
            }
            else if (item.index > 100)
            {
                if (itemList[i].item.index == item.index)
                {
                    int remainingQuantity = maxItemLenght - itemList[i].count;
                    if (remainingQuantity > 0)
                    {
                        if (item.count <= remainingQuantity)
                        {
                            itemList[i].count += item.count;
                            itemList[i].SetData(item);
                            return;
                        }
                        else
                        {
                            itemList[i].count += remainingQuantity;
                            item.count -= remainingQuantity;
                        }
                    }
                }
            }
        }
        
    }
    public void Remove(SOItem item)
    {
        //itemList.Remove(item);
    }
    public void ListItems()
    {
        foreach (Transform item in ItemContent) // 이게 없으면 복제가됨
        {
            Destroy(item.gameObject);
        }

        foreach (var item in itemList)
        {
            GameObject obj = Instantiate(InventoryItem, ItemContent);
            var ItemCount = obj.transform.Find("ItemCount").GetComponent<Text>();
            var itemIcon = obj.transform.Find("ItemIcon").GetComponent<Image>();
            var removeButton = obj.transform.Find("RemoveButton").GetComponent<Button>();

            ItemCount.text = item.count.ToString();
            itemIcon.sprite = item.item.icon;
            if (EnableRemove.isOn)
            {
                removeButton.gameObject.SetActive(true);
            }
        }
    }
    public void EnableItemsRemove()
    {
        if (EnableRemove.isOn)
        {
            foreach (Transform item in ItemContent)
            {
                item.Find("RemoveButton").gameObject.SetActive(true);
            }
        }
        else
        {
            foreach (Transform item in ItemContent)
            {
                item.Find("RemoveButton").gameObject.SetActive(false);
            }
        }
    }
}
