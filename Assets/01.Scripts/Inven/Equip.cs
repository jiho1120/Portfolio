using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Equip : MonoBehaviour
{
    public AllEnum.ItemType itemType;
    public int exp;
    public int maxExp;

    public Text level;

    private void Start()
    {
        level = transform.Find("ItemLevel").GetComponent<Text>();
        level.text = "Lv : " + 1;

    }
    public void SetData()
    {

    }


}
