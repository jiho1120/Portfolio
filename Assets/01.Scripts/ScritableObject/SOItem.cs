using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SOItem: ScriptableObject
{
    public int index; 
    public int level = 1; // 을 올려서 능력치 올리는 함수 만들꺼임 , 이거 따라서 슬롯도 나뉨
    public AllEnum.ItemType itemType;
    public Sprite icon; 
    public float health;
    public float mana;
    public float ultimateGauge;
    public float defense;
    public float maxHealth;
    public float luck;
    public float attack;
    public float criticalChance;
    public float maxMana;
    public float movementSpeed;
}
