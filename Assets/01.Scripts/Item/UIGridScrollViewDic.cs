using UnityEngine;
using UnityEngine.UI;


public class UIGridScrollViewDic : MonoBehaviour
{
    public UIGridScrollView scrollView;
    public Button btnTestGetItem;

    public UIEquipmentView equipmentView;
    public UIPopUpItemDetail popupDetail;


    public void Init()
    {
        popupDetail.onSell = (id) =>
        {
            Debug.Log("sellItem : " + id);
            SellItem(id);
        };
        popupDetail.onEquip = (item) =>
        {
            EquipItem(item);

        };
        scrollView.onFocus = (index) =>
        {
            //popupDetail.Init(index).Open();
            popupDetail.Init(index);

        };

        btnTestGetItem.onClick.AddListener(() =>
        {
            //랜덤 아이템 획득
            ItemData data = new ItemData();

            InvenManager.Instance.AdditemToInven(data.GetRandomItemData());

            //저장
            DataManager.Instance.SaveInvenInfo();

            scrollView.Refresh();
        });
        scrollView.Init();
    }

    void SellItem(int id)
    {
        var info = DataManager.Instance.gameData.invenDatas.invenItemDatas.Find(x => x.index == id);

        if (info.count > 1)
        {
            --info.count;
            popupDetail.Refresh();
        }
        else
        {
            DataManager.Instance.gameData.invenDatas.invenItemDatas.Remove(info);

            popupDetail.Close();
        }
        DataManager.Instance.SaveInvenInfo();

        scrollView.Refresh();
    }

    public void EquipItem(ItemData item)
    {
        if (item.index == -1) // 이건 안전용으로 둔거임 필요없긴함
        {
            return;
        }
        else if (item.index <= 100)
        {
            ItemData equippedItem = DataManager.Instance.gameData.invenDatas.EquipItemDatas[item.itemList];

            ItemData cloneItem = new ItemData(item);
            cloneItem.count = 1;

            //장착 아이템 인덱스가 -1
            if (equippedItem.index == -1)
            {
                DataManager.Instance.gameData.invenDatas.EquipItemDatas[item.itemList] = cloneItem;
                --item.count;
            }
            // 인덱스 -1 아닐때
            else if (equippedItem.index != -1)
            {
                if (equippedItem.index == item.index && equippedItem.level == item.level)
                {
                    return;
                }
                else if (equippedItem.index != item.index && equippedItem.level != item.level)
                {
                    InvenManager.Instance.AdditemToInven(equippedItem);
                    DataManager.Instance.gameData.invenDatas.EquipItemDatas[item.itemList] = cloneItem;
                    --item.count;
                }
            }

            if (item.count <= 0)
            {
                DataManager.Instance.gameData.invenDatas.invenItemDatas.Remove(item);
                popupDetail.Close();

            }
            // 장착한 아이템의 카운트가 0이 아닌 경우에만 인벤을 갱신합니다.
            if (item.count > 0)
            {
                // 인벤 갱신
                popupDetail.Refresh();
            }
        }
        else if (item.index > 100) // 101부터 물약
        {
            Debug.Log("더하기 직전 인덱스 : " + item.index);

            InvenManager.Instance.AddToPosionInven(item);

            popupDetail.Close();

        }

        // 저장
        DataManager.Instance.SaveInvenInfo();

        // ScrollView 및 EquipmentView 갱신
        scrollView.Refresh();
        equipmentView.Init();
        GameManager.Instance.player.ApplyEquipmentStat();
        UIManager.Instance.SetPlayerUI();
    }


}
