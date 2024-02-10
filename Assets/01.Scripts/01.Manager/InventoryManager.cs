using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class InventoryManager : Singleton<InventoryManager>,ReInitialize
{
    public List<ItemSlot> itemList = new List<ItemSlot>();
    public List<ItemSlot> playerItemList = new List<ItemSlot>();


    int maxItemListLenght = 50;
    int maxItemLenght = 99;
    public Equip[] equipList;
    public SOItem emptyData;

    public Transform ItemContent;
    public GameObject InventoryItem;
    public Toggle EnableRemove;

    public GameObject inven;
    public bool invenOn = false;
    public void Init()
    {
        SetItemListCount();
        for (int i = 0; i < equipList.Length; i++)
        {
            equipList[i].Init();
        }
    }

    public void ReInit()
    {
        throw new System.NotImplementedException();
    }

    public void Deactivate()
    {
        throw new System.NotImplementedException();
    }



    public void SetItemListCount()
    {
        for (int i = 0; i < maxItemListLenght; i++)
        {
            ItemSlot obj = Instantiate(InventoryItem, ItemContent).GetComponent<ItemSlot>();
            obj.slotIndex = i;
            itemList.Add(obj);
        }
    }
    public void AddItemListCount(int num)
    {
        maxItemListLenght += num;
    }

    public void InvenOnOff()
    {
        invenOn = !invenOn;
        if (invenOn)
        {
            Time.timeScale = 0f; // 시간의 흐름이 멈춤  //코루틴 안되고, 업데이트 안 되고 , 픽스드 가능, 드래그도 가능
            GameManager.Instance.StopNum++;
            inven.SetActive(true);
            SetItemsInfo();
            GameManager.Instance.LockCursor(false);
        }
        else
        {
            GameManager.Instance.StopNum--;

            if (GameManager.Instance.StopNum == 0)
            {
                Time.timeScale = 1f;
            }
            inven.SetActive(false);
            GameManager.Instance.LockCursor(true);

        }
    }
    public bool checkAdd(SOItem item)
    {
        for (int i = 0; i < itemList.Count; i++)
        {
            if (itemList[i].item.index == -1)
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
        return false;
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
    public void AllDataRemove()
    {
        for (int i = 0; i < itemList.Count; i++)
        {
            Remove(itemList[i]);
        }
    }
    public void Remove(ItemSlot item)
    {
        item.item = emptyData;
        item.count = 0;
        item.countTxt.text = item.count.ToString();
        item.icon.sprite = null;
    }
    public void Remove(AllEnum.ItemType item)
    {
        int num = 0;
        for (int i = 0; i < itemList.Count; i++)
        {
            if (num >= 3)
            {
                return;
            }

            if (itemList[i].item.itemType == item)
            {
                itemList[i].item = emptyData;
                itemList[i].count = 0;
                itemList[i].countTxt.text = "0";
                itemList[i].icon.sprite = null;
                num++;
            }
        }
    }
    public void SetItemsInfo()
    {
        for (int i = 0; i < itemList.Count; i++)
        {
            itemList[i].icon.sprite = itemList[i].item.icon;
            itemList[i].countTxt.text = itemList[i].count.ToString();
            if (EnableRemove.isOn)
            {
                itemList[i].removeButton.gameObject.SetActive(true);
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

    public void SwapItems(ItemSlot slotA, ItemSlot slotB)
    {
        int count = slotB.count;
        SOItem item = slotB.item;
        slotB.SetSlotData(slotA.count, slotA.item);
        slotA.SetSlotData(count, item);

    }

    public void UseItem(int num)
    {
        if (playerItemList[num].count > 0)
        {
            if (playerItemList[num].item.itemType == AllEnum.ItemType.HpPosion)
            {
                GameManager.Instance.player.SetHp(GameManager.Instance.player.Hp+playerItemList[num].item.health);

                Debug.Log(GameManager.Instance.player.playerStat.health);

            }
            else if (playerItemList[num].item.itemType == AllEnum.ItemType.MpPosion)
            {
                GameManager.Instance.player.SetMp(GameManager.Instance.player.Mp + (playerItemList[num].item.mana));
                Debug.Log(GameManager.Instance.player.playerStat.mana);

            }
            else if (playerItemList[num].item.itemType == AllEnum.ItemType.UltimatePosion)
            {
                GameManager.Instance.player.playerStat.AddUltimateGauge(playerItemList[num].item.ultimateGauge);
                Debug.Log(GameManager.Instance.player.playerStat.ultimateGauge);

            }
            else
            {
                return;
            }
            playerItemList[num].count -= 1;
            playerItemList[num].SetSlotDataCount(playerItemList[num].count);
        }
        else
        {
            UiManager.Instance.OpenWarning("장착된 아이템이 없음");
        }

    }

    
}
