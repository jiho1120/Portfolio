using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class UIGridCellView : MonoBehaviour
{
    public int index;
    public Image imgIcon;
    public TMP_Text txtAmount;
    public GameObject focusGo;
    
    public void Init(ItemData item)
    {
        index = item.index;
        //imgIcon.sprite = item.icon;
        for (int i = 0; i < DataManager.Instance.soItem.Length; i++)
        {
            if (index == DataManager.Instance.soItem[i].index)
            {
                imgIcon.sprite = DataManager.Instance.soItem[i].icon;
                break;
            }
        }
        //imgIcon.SetNativeSize();
        txtAmount.text = item.count.ToString();
        txtAmount.gameObject.SetActive(item.count > 1);
    }
    public void Focus(bool active)
    {
        focusGo.SetActive(active);
    }
    
}
