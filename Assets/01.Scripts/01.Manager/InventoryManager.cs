using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class InventoryManager : Singleton<InventoryManager>
{
    public List<Item> itemList = new List<Item>(); // ������ �ٲ��ȵ�
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
            Time.timeScale = 0f; // �ð��� �帧�� ����  //�ڷ�ƾ �ȵǰ�, ������Ʈ �� �ǰ� , �Ƚ��� ����, �巡�׵� ����
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
        Debug.Log("�Լ� �Ҹ�");

        if (item.itemData.index < 101)
        {
            Debug.Log("100���� ����");
            itemList.Add(item);
        }
        else
        {
            bool itemAdded = false;

            for (int i = 0; i < itemList.Count; i++)
            {
                Debug.Log("100���� ŭ");
                if (item.itemData.index == itemList[i].itemData.index)
                {
                    Debug.Log("������");

                    int remainingSpace = 99 - itemList[i].haveCount;

                    if (remainingSpace > 0)
                    {
                        // ���� ������ ����Ʈ�� ���� �� �ִ� ������ �������� ��
                        if (remainingSpace >= item.haveCount)
                        {
                            // �������� �߰��ϰ� ���� ����
                            itemList[i].AddCount(item.haveCount);;
                            itemAdded = true;
                            break;
                        }
                        else
                        {
                            // �������� �Ϻθ� �߰��ϰ� ���� ���� ������Ʈ
                            itemList[i].SetCount(99);
                            item.SetCount(item.haveCount - remainingSpace);
                        }
                    }
                }
            }

            // �������� ����Ʈ�� �߰����� �ʾҴٸ� ���ο� ���������� �߰�
            if (!itemAdded)
            {
                Debug.Log("�������");
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
        foreach (Transform item in ItemContent) // �̰� ������ ��������
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
