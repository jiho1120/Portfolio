using UnityEngine;

[CreateAssetMenu]
public class SOStat : ScriptableObject
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
    public float luck;
    public float maxExperience;
    public float ultimateGauge;
    public float maxUltimateGauge;
}
