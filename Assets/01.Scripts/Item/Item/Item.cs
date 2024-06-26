using UnityEngine;
using UnityEngine.Pool;

public class Item : MonoBehaviour
{
    public ItemData itemData; // public 선언 해야함
    [SerializeField] SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    public void SetItemData(int idx)
    {
        itemData.index = idx;
        for (int i = 0; i < DataManager.Instance.soItem.Length; i++)
        {
            if (DataManager.Instance.soItem[i].index == idx)
            {
                itemData.SetItemData(DataManager.Instance.soItem[i]);
            }
        }
    }

    

    
}
