using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public int index { get; private set; }
    [SerializeField] SpriteRenderer spriteRenderer;
    private void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }
    
    public void SetItemData(int idx)
    {
        index = idx;
        if (index <= 100)
        {
            for (int i = 0; i < DataManager.Instance.Equipment.Length; i++)
            {
                if (DataManager.Instance.Equipment[i].index == idx)
                {
                    spriteRenderer.sprite = DataManager.Instance.Equipment[i].icon;
                }
            }
        }
        else
        {
            for (int i = 0; i < DataManager.Instance.Posion.Length; i++)
            {
                if (DataManager.Instance.Posion[i].index == idx)
                {
                    spriteRenderer.sprite = DataManager.Instance.Posion[i].icon;
                }
            }
        }
    }

    
}
