using UnityEngine;
using static AllEnum;
using static UnityEditor.Progress;

public class InvenManager : Singleton<InvenManager>
{
    public ItemData GetInvenItemData(ItemData itemData)
    {
        ItemData _itemdata = DataManager.Instance.gameData.invenDatas.invenItemDatas.Find(x => x.index == itemData.index && x.level == itemData.level);

        return _itemdata;
    }

    public ItemData GetPosionInvenItemData(ItemData itemData)
    {
        ItemData _itemdata = DataManager.Instance.gameData.invenDatas.PosionItemDatas[itemData.itemList];

        return _itemdata;
    }

    public void AdditemToInven(ItemData itemData)
    {
        if (itemData.index == -1)
        {
            return;
        }
        ItemData _itemData = GetInvenItemData(itemData);

        if (itemData == null)
        {
            Debug.Log(itemData + "��");
        }
        else if (_itemData == null)
        {
            DataManager.Instance.gameData.invenDatas.invenItemDatas.Add(itemData);
        }
        else
        {
            if (_itemData.count < 99)
            {
                _itemData.count++;
            }
            else
            {
                DataManager.Instance.gameData.invenDatas.invenItemDatas.Add(itemData);
            }
        }
    }
    public void AddToPosionInven(ItemData itemData)
    {
        Debug.Log("���ϱ� ���ͼ� �ε��� : " + itemData.index);
        if (itemData.index == -1)
        {
            return;
        }
        ItemData _itemData = GetPosionInvenItemData(itemData);
        if (_itemData.count >= 99)
        {
            return; // 99 �̻� ������
        }
        else if (_itemData.index == -1) // �⺻���� �־ null�ϼ��� ����
        {
            _itemData.SetItemData(itemData);
            DataManager.Instance.gameData.invenDatas.invenItemDatas.Remove(itemData);
        }
        else if (_itemData.count < 99)// ���ʿ� ��������������
        {
            _itemData.count += itemData.count;
            if (_itemData.count > 99)
            {
                itemData.count = _itemData.count - 99;
                _itemData.count = 99;
                // �������� �κ��� 99�� �Ʒ��� ���� ���ǰ� ��������
                AdditemToInven(itemData);
            }
            else if (_itemData.count <= 99)
            {
                DataManager.Instance.gameData.invenDatas.invenItemDatas.Remove(itemData);
            }
        }
        DataManager.Instance.SaveInvenInfo();
    }


    public void AddEquipItemToEquipDatas(ItemList itemList, ItemData itemData)
    {
        DataManager.Instance.gameData.invenDatas.EquipItemDatas[itemList] = itemData;
    }

    public void SetPlayerStatWithEquipItem()
    {

    }

}
