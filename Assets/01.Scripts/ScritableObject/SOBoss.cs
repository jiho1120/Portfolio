using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SOBoss : ScriptableObject
{
    public AllEnum.ObjectType objectType;
    public int level;
    public float health;
    public float maxHealth;
    public float attack;
    public float defense;
    public float criticalChance;
    public float movementSpeed;
    public float experience;
    public int money;
    public float mana;
    public float maxMana;
}
