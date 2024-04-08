using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIPopUpItemDetail : MonoBehaviour
{
    public TMP_Text txtItemType;
    public Image imgItemIcon;
    public TMP_Text txtItemName;
    public Button btnSell;
    public TMP_Text txtSellPrice;
    public TMP_Text txtSellCount;


    public Action<int> onSell;
    int id;
    /// <summary>
    /// 오픈전에 반드시 부르기
    /// </summary>
    public UIPopUpItemDetail Init(int id)
    {
        this.id = id;
        var data = DataManager.Instance.gameData.invenDatas.GetItemDataForIndex(id);
        var type = data.itemList;
        txtItemType.text = type.ToString();// 나중에 타입으로 바꾸기
        var sprite = data.icon;
        imgItemIcon.sprite = sprite;
        txtItemName.text = data.itemList.ToString();

        //txtSellPrice.text = string.Format(data.price); //나중에 추가
        txtSellCount.text = data.count.ToString();

        btnSell.onClick.AddListener(OnSellActionHandler);
        btnSell.gameObject.SetActive(true);

        return this;
    }
    void OnSellActionHandler()
    {
        // 이벤트 발송
        onSell(this.id);
    }

    public void Open() //나중에 많아지면 베이스를 만들고 UI들을 상속시키면 한개만 만들어도됨
    {
        gameObject.SetActive(true);
    }
    public void Close()
    {
        gameObject.SetActive(false);
        btnSell.onClick.RemoveListener(OnSellActionHandler);
    }
}
