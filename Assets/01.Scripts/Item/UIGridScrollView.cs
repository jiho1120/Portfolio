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
        //가지고 있는 아이템의 갯수 만큼 생성
        CreateCellViews();
        GetComponent<ScrollRect>().vertical = DataManager.Instance.gameData.invenDatas.invenItemDatas.Count > THRESHOLD;
    }

    public void Refresh()
    {
        //새로운 데이터 로드
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

                // 이벤트 전송
                onFocus(this.currentFocusCellView.index);
            });
            //id, 아이콘, 수량
            ItemData info = DataManager.Instance.gameData.invenDatas.invenItemDatas[i];

            cellview.Init(info);
        }
    }
}
