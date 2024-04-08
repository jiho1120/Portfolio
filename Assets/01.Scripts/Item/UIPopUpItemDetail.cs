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
    /// �������� �ݵ�� �θ���
    /// </summary>
    public UIPopUpItemDetail Init(int id)
    {
        this.id = id;
        var data = DataManager.Instance.gameData.invenDatas.GetItemDataForIndex(id);
        var type = data.itemList;
        txtItemType.text = type.ToString();// ���߿� Ÿ������ �ٲٱ�
        var sprite = data.icon;
        imgItemIcon.sprite = sprite;
        txtItemName.text = data.itemList.ToString();

        //txtSellPrice.text = string.Format(data.price); //���߿� �߰�
        txtSellCount.text = data.count.ToString();

        btnSell.onClick.AddListener(OnSellActionHandler);
        btnSell.gameObject.SetActive(true);

        return this;
    }
    void OnSellActionHandler()
    {
        // �̺�Ʈ �߼�
        onSell(this.id);
    }

    public void Open() //���߿� �������� ���̽��� ����� UI���� ��ӽ�Ű�� �Ѱ��� ������
    {
        gameObject.SetActive(true);
    }
    public void Close()
    {
        gameObject.SetActive(false);
        btnSell.onClick.RemoveListener(OnSellActionHandler);
    }
}
