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
            //���� ������ ȹ��
            ItemData data = new ItemData();

            InvenManager.Instance.AdditemToInven(data.GetRandomItemData());

            //����
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
        if (item.index == -1) // �̰� ���������� �а��� �ʿ������
        {
            return;
        }
        else if (item.index <= 100)
        {
            ItemData equippedItem = DataManager.Instance.gameData.invenDatas.EquipItemDatas[item.itemList];

            ItemData cloneItem = new ItemData(item);
            cloneItem.count = 1;

            //���� ������ �ε����� -1
            if (equippedItem.index == -1)
            {
                DataManager.Instance.gameData.invenDatas.EquipItemDatas[item.itemList] = cloneItem;
                --item.count;
            }
            // �ε��� -1 �ƴҶ�
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
            // ������ �������� ī��Ʈ�� 0�� �ƴ� ��쿡�� �κ��� �����մϴ�.
            if (item.count > 0)
            {
                // �κ� ����
                popupDetail.Refresh();
            }
        }
        else if (item.index > 100) // 101���� ����
        {
            Debug.Log("���ϱ� ���� �ε��� : " + item.index);

            InvenManager.Instance.AddToPosionInven(item);

            popupDetail.Close();

        }

        // ����
        DataManager.Instance.SaveInvenInfo();

        // ScrollView �� EquipmentView ����
        scrollView.Refresh();
        equipmentView.Init();
        GameManager.Instance.player.ApplyEquipmentStat();
        UIManager.Instance.SetPlayerUI();
    }


}
