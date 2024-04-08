using UnityEngine;
using UnityEngine.UI;


public class UIGridScrollViewDic : MonoBehaviour
{
    public UIGridScrollView scrollView;
    public Button btnTestGetItem;

    public UIPopUpItemDetail popupDetail;
    public void Init()
    {
        popupDetail.onSell = (id) =>
        {
            Debug.Log("sellItem : " + id);
            SellItem(id);
        };
        scrollView.onFocus = (index) =>
        {
            popupDetail.Init(index).Open();
        };

        //btnTestGetItem.onClick.AddListener(() =>
        //{
        //    //·£´ý ¾ÆÀÌÅÛ È¹µæ
        //   var data = DataManager.Instance.GetRandomItemData();

        //    InvenManager.Instance.Additem(data);

        //    //ÀúÀå
        //    DataManager.Instance.SaveInvenInfo(DataManager.Instance.gameData.invenDatas);

        //    scrollView.Refresh();
        //});
        scrollView.Init();
    }

    void SellItem(int id)
    {
        var info = DataManager.Instance.gameData.invenDatas.invenItemDatas.Find(x => x.index == id);

        if (info.count > 1)
        {
            --info.count;
        }else 
        {
            DataManager.Instance.gameData.invenDatas.invenItemDatas.Remove(info);

            popupDetail.Close();
        }
        DataManager.Instance.SaveInvenInfo(DataManager.Instance.gameData.invenDatas);

        scrollView.Refresh();

    }
}
