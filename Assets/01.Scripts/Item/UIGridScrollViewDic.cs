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
            Debug.Log("EquipItem : " + item.index);
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

            InvenManager.Instance.Additem(data.GetRandomItemData());

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
        // 장착할 아이템을 찾습니다.
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
                InvenManager.Instance.Additem(equippedItem);
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

        // 저장
        DataManager.Instance.SaveInvenInfo();

        // ScrollView 및 EquipmentView 갱신
        scrollView.Refresh();
        equipmentView.Init();
        GameManager.Instance.player.ApplyEquipmentStat();
    }

}
