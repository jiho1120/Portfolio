using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SOPlayer : ScriptableObject
{
    public AllEnum.ObjectType objectType;
    public int level;
    public float health;
    public float maxHealth;
    public float attack;
    public float defense;
    public float criticalChance;
    public float movementSpeed;
    public float mana;
    public float maxMana;
    public float luck;
    public float experience;
    public float maxExperience;
    public float ultimateGauge;
    public float maxUltimateGauge;
}
