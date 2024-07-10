using UnityEngine;

public class Item : MonoBehaviour
{
    public ItemData itemData; // public 선언 해야함
    [SerializeField] SpriteRenderer spriteRenderer;
    

    private void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }
}
