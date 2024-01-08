using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class InventoryManager : Singleton<InventoryManager>
{
    public List<Item> itemList = new List<Item>(); // 무조건 바뀌면안돼
    public Equip[] equipList;

    public Transform ItemContent;
    public GameObject InventoryItem;
    public Toggle EnableRemove;
    public ItemSlot[] inventoryItems;

    public GameObject inven;
    public bool invenOn = false;
    void Start()

    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void InvenOnOff()
    {
        invenOn = !invenOn;
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
    public void AddItem(Item item)
    {
        Debug.Log("함수 불림");

        if (item.itemData.index < 101)
        {
            Debug.Log("100보다 작음");
            itemList.Add(item);
        }
        else
        {
            bool itemAdded = false;

            for (int i = 0; i < itemList.Count; i++)
            {
                Debug.Log("100보다 큼");
                if (item.itemData.index == itemList[i].itemData.index)
                {
                    Debug.Log("존재함");

                    int remainingSpace = 99 - itemList[i].haveCount;

                    if (remainingSpace > 0)
                    {
                        // 현재 아이템 리스트에 더할 수 있는 공간이 남아있을 때
                        if (remainingSpace >= item.haveCount)
                        {
                            // 아이템을 추가하고 루프 종료
                            itemList[i].AddCount(item.haveCount);;
                            itemAdded = true;
                            break;
                        }
                        else
                        {
                            // 아이템을 일부만 추가하고 남은 개수 업데이트
                            itemList[i].SetCount(99);
                            item.SetCount(item.haveCount - remainingSpace);
                        }
                    }
                }
            }

            // 아이템이 리스트에 추가되지 않았다면 새로운 아이템으로 추가
            if (!itemAdded)
            {
                Debug.Log("존재안함");
                itemList.Add(item);
            }
        }
    }
    public void Remove(Item item)
    {
        itemList.Remove(item);
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

            ItemCount.text = item.haveCount.ToString();
            itemIcon.sprite = item.itemData.icon;
            if (EnableRemove.isOn)
            {
                removeButton.gameObject.SetActive(true);
            }
        }
        SetInventoryItems();
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

    public void SetInventoryItems()
    {
        inventoryItems = ItemContent.GetComponentsInChildren<ItemSlot>();
        for (int i = 0; i < itemList.Count; i++)
        {
            inventoryItems[i].AddItem(itemList[i]);
        }
    }

}
