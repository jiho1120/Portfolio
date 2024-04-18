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
    public GameObject StatPrefab; // groupStats밑에 들어가는 능력치 프리팹

    [Header("Group_Menu")]
    public Button btnSell;
    public TMP_Text txtSellPrice;
    public Button btnFuse;
    public Button btnEquip;


    public Action<int> onSell;
    int id;
    /// <summary>
    /// 오픈전에 반드시 부르기
    /// </summary>
    public UIPopUpItemDetail Init(int id)
    {
        this.id = id;
        ItemData data = DataManager.Instance.gameData.invenDatas.GetItemDataForIndex(id);
        var type = data.itemList;
        txtItemType.text = type.ToString();// 나중에 타입으로 바꾸기
        var sprite = data.icon;
        imgItemIcon.sprite = sprite;
        txtItemName.text = data.itemList.ToString();

        //txtSellPrice.text = string.Format(data.price); //나중에 추가
        txtItemCount.text = data.count.ToString();

        btnSell.onClick.AddListener(OnSellActionHandler);
        btnSell.gameObject.SetActive(true);

        if (data.index < 100) // 장비
        {
            groupStats.SetActive(true);

        }
        else //물약
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
        // 이벤트 발송
        onSell(this.id);
    }

    public override void Close()
    {
        base.Close();
        btnSell.onClick.RemoveListener(OnSellActionHandler);
    }
}
