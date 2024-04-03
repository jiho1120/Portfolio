using UnityEngine;

[CreateAssetMenu]
public class SOItem : ScriptableObject
{
    public int index;
    public int level = 1;
    public int count = 1;
    public AllEnum.ItemList itemType;
    public Sprite icon;
    public float hp;
    public float mp;
    public float ultimateGauge;
    public float defense;
    public float maxHp;
    public float luck;
    public float attack;
    public float critical;
    public float maxMp;
    public float speed;
}