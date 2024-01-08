using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Equip : MonoBehaviour
{
    public AllEnum.ItemType itemType;
    public int exp;
    public int maxExp =10;
    int lv = 1;
    public Text level;

    private void Start()
    {
        level = transform.Find("ItemLevel").GetComponent<Text>();
        level.text = "Lv : " + lv;
    }
    public void GetExp(int val)
    {
        exp += val;
    }
    public void LevelUp()
    {
        lv += 1;
        level.text = "Lv : " + lv;
        exp = exp - maxExp;
        maxExp += 10;
    }



}
