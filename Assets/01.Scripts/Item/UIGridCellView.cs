using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class UIGridCellView : MonoBehaviour
{
    public int index;
    public Image imgIcon;
    public TMP_Text txtAmount;
    public GameObject focusGo;
    public void Init(int id,Sprite sprite, int amount)
    {
        index = id;
        imgIcon.sprite = sprite;
        txtAmount.text = amount.ToString();
    }
    public void Init(ItemData item)
    {
        index = item.index;
        imgIcon.sprite = item.icon;
        //imgIcon.SetNativeSize();
        txtAmount.text = item.count.ToString();
        txtAmount.gameObject.SetActive(item.count > 1);
    }
    public void Focus(bool active)
    {
        focusGo.SetActive(active);
    }
}
