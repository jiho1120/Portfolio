using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIPopUpItemDetail : BasicPopUp
{
    [Header("Group_ItemInfo")]
    public TMP_Text txtItemType;
    public Image imgItemIcon;
    public TMP_Text txtItemName;
    public TMP_Text txtItemCount;


    [Header("Group_Stats")]
    public GameObject groupStats;
    public GameObject StatPrefab; // groupStats�ؿ� ���� �ɷ�ġ ������

    [Header("Group_Menu")]
    public Button btnSell;
    public TMP_Text txtSellPrice;
    public Button btnFuse;
    public Button btnEquip;


    public Action<int> onSell;
    public Action<ItemData> onEquip;

    int id;
    ItemData data;
    /// <summary>
    /// �������� �ݵ�� �θ���
    /// </summary>
    public UIPopUpItemDetail Init(int id)
    {
        this.id = id;
        
        data = DataManager.Instance.gameData.invenDatas.GetItemDataForIndex(id);
        var type = data.itemList;
        txtItemType.text = type.ToString();// ���߿� Ÿ������ �ٲٱ�
        var sprite = ResourceManager.Instance.ItemSprite[(int)data.itemList];
        imgItemIcon.sprite = sprite;
        txtItemName.text = data.itemList.ToString();

        //txtSellPrice.text = string.Format(data.price); //���߿� �߰�
        txtItemCount.text = data.count.ToString();

        btnSell.onClick.AddListener(OnSellActionHandler);
        btnSell.gameObject.SetActive(true);
        btnEquip.onClick.AddListener(OnEquipActionHandler);
        btnEquip.gameObject.SetActive(true);

        if (data.index < 100) // ���
        {
            groupStats.SetActive(true);

        }
        else //����
        {
            groupStats.SetActive(false);
        }
        
            Open();
        return this;
    }
    public void Refresh()
    {
        ItemData data = DataManager.Instance.gameData.invenDatas.GetItemDataForIndex(id);
        txtItemCount.text = data.count.ToString();
    }
    void OnSellActionHandler()
    {
        // �̺�Ʈ �߼�
        onSell(this.id);
    }
    void OnEquipActionHandler()
    {
        // �̺�Ʈ �߼�
        onEquip(data);
    }

    public override void Close()
    {
        base.Close();
        btnSell.onClick.RemoveListener(OnSellActionHandler);
        btnEquip.onClick.RemoveListener(OnEquipActionHandler);
    }
}
