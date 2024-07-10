using UnityEngine;

public class Item : MonoBehaviour
{
    public ItemData itemData; // public ���� �ؾ���
    [SerializeField] SpriteRenderer spriteRenderer;
    

    private void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }
}
