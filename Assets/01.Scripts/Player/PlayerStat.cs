using UnityEngine;

public class PlayerStat : BossStat
{
    public float luck { get; private set; }
    public float experience { get; private set; }
    public float maxExperience { get; private set; }
    public float ultimateGauge { get; private set; }
    public float maxUltimateGauge { get; private set; }

    public PlayerStat() : base()
    {

    }

    public PlayerStat(AllEnum.ObjectType objectType, int level, float health, float maxHealth, float attack, float defense, float criticalChance, float movementSpeed, float mana, float maxMana, float luck, float experience, float maxExperience, float ultimateGauge, float maxUltimateGauge)
        : base(objectType, level, health, maxHealth, attack, defense, criticalChance, movementSpeed, mana, maxMana)
    {
        this.luck = luck;
        this.experience = experience;
        this.maxExperience = maxExperience;
        this.ultimateGauge = ultimateGauge;
        this.maxUltimateGauge = maxUltimateGauge;
    }

    public void SetValues(AllEnum.ObjectType objectType, int level, float health, float maxHealth, float attack, float defense, float criticalChance, float movementSpeed, float mana, float maxMana, float luck, float experience, float maxExperience, float ultimateGauge, float maxUltimateGauge)
    {
        base.SetValues(objectType, level, health, maxHealth, attack, defense, criticalChance, movementSpeed, mana, maxMana);
        this.luck = luck;
        this.experience = experience;
        this.maxExperience = maxExperience;
        this.ultimateGauge = ultimateGauge;
        this.maxUltimateGauge = maxUltimateGauge;
    }

    public void SetValues()
    {
        this.luck = luck;
        this.experience = experience;
        this.maxExperience = maxExperience;
        this.ultimateGauge = ultimateGauge;
        this.maxUltimateGauge = maxUltimateGauge;
    }
    public void SetValues(SOPlayer soPlayer)
    {
        base.SetValues(soPlayer.objectType, soPlayer.level, soPlayer.health, soPlayer.maxHealth, soPlayer.attack, soPlayer.defense, soPlayer.criticalChance, soPlayer.movementSpeed, soPlayer.mana, soPlayer.maxMana);
        this.luck = soPlayer.luck;
        this.experience = soPlayer.experience;
        this.maxExperience = soPlayer.maxExperience;
        this.ultimateGauge = soPlayer.ultimateGauge;
        this.maxUltimateGauge = soPlayer.maxUltimateGauge;
    }
    public void ShowInfo()
    {
        Debug.Log(this.objectType);
        Debug.Log(this.level);
        Debug.Log(this.health);
        Debug.Log(this.maxHealth);
        Debug.Log(this.attack);
        Debug.Log(this.defense);
        Debug.Log(this.criticalChance);
        Debug.Log(this.movementSpeed);
        Debug.Log(this.mana);
        Debug.Log(this.maxMana);
        Debug.Log(this.luck);
        Debug.Log(this.experience);
        Debug.Log(this.maxExperience);
        Debug.Log(this.ultimateGauge);
        Debug.Log(this.maxUltimateGauge);

    }
}
