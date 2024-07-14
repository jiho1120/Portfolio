using System;
using UnityEngine;
using UnityEngine.UI;

public class UIGridScrollView : MonoBehaviour
{
    public Transform content;
    public GameObject cellviewprefab;
    public GameObject txtNoItemsGo;
    const int THRESHOLD = 6;

    private UIGridCellView currentFocusCellView;
    public Action<int> onFocus;
    public void Init()
    {
        txtNoItemsGo.SetActive(DataManager.Instance.gameData.invenDatas.invenItemDatas.Count == 0);
        //������ �ִ� �������� ���� ��ŭ ����
        CreateCellViews();
        GetComponent<ScrollRect>().vertical = DataManager.Instance.gameData.invenDatas.invenItemDatas.Count > THRESHOLD;
    }

    public void Refresh()
    {
        //���ο� ������ �ε�
        DataManager.Instance.LoadInvenInfo();

        foreach (Transform child in content)
        {
            Destroy(child.gameObject);
        }

        CreateCellViews();
        GetComponent<ScrollRect>().vertical = DataManager.Instance.gameData.invenDatas.invenItemDatas.Count > THRESHOLD;
        txtNoItemsGo.SetActive(DataManager.Instance.gameData.invenDatas.invenItemDatas.Count == 0);
    }
    public void CreateCellViews()
    {
        for (int i = 0; i < DataManager.Instance.gameData.invenDatas.invenItemDatas.Count; i++)
        {
            var go = Instantiate(cellviewprefab, content);
            var cellview = go.GetComponent<UIGridCellView>();
            var btn = go.GetComponent<Button>();
            btn.onClick.AddListener(() =>
            {
                if (this.currentFocusCellView != null)
                {
                    this.currentFocusCellView.Focus(false);
                }

                cellview.Focus(true);
                this.currentFocusCellView = cellview;

                // �̺�Ʈ ����
                onFocus(this.currentFocusCellView.index);
            });
            //id, ������, ����
            ItemData info = DataManager.Instance.gameData.invenDatas.invenItemDatas[i];

            cellview.Init(info);
        }
    }
}
