using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SOItem: ScriptableObject
{
    public int index; 
    public int level = 1; // �� �÷��� �ɷ�ġ �ø��� �Լ� ���鲨�� , �̰� ���� ���Ե� ����
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
