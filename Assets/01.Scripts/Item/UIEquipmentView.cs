using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIEquipmentView : MonoBehaviour
{
    public EquipSlot[] equipSlots;
    public void Init()
    {
        for (int i = 0; i < equipSlots.Length; i++)
        {
            equipSlots[i].SetEquipSlotData();
        }
    }

}
