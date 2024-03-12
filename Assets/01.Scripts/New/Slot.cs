using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Slot : MonoBehaviour
{
    public Text nameText;
    public Text dateTimeText;

    public void UIUpdate(int idx)
    {
        nameText.text = SlotManager.Instance.slotDatas[idx].playerName;   
        dateTimeText.text = SlotManager.Instance.slotDatas[idx].date + SlotManager.Instance.slotDatas[idx].time;
    }
}
