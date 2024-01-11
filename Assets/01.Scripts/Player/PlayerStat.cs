using UnityEngine;

public class PlayerStat : UseManaStat
{
    public float luck { get; private set; }
    public float maxExperience { get; private set; }
    public float ultimateGauge { get; private set; }
    public float maxUltimateGauge { get; private set; }

    public PlayerStat() : base()
    {

    }

    public PlayerStat(AllEnum.ObjectType objectType, int level, float health, float maxHealth, float attack, float defense, float criticalChance, float movementSpeed, float experience, int money, float mana, float maxMana, float luck, float maxExperience, float ultimateGauge, float maxUltimateGauge)
        : base(objectType, level, health, maxHealth, attack, defense, criticalChance, movementSpeed, experience, money, mana, maxMana)
    {
        this.luck = luck;
        this.maxExperience = maxExperience;
        this.ultimateGauge = ultimateGauge;
        this.maxUltimateGauge = maxUltimateGauge;
    }

    public void SetValues(AllEnum.ObjectType objectType, int level, float health, float maxHealth, float attack, float defense, float criticalChance, float movementSpeed, float experience,int money, float mana, float maxMana, float luck, float maxExperience, float ultimateGauge, float maxUltimateGauge)
    {
        base.SetValues(objectType, level, health, maxHealth, attack, defense, criticalChance, movementSpeed, experience, money, mana, maxMana);
        this.luck = luck;
        this.maxExperience = maxExperience;
        this.ultimateGauge = ultimateGauge;
        this.maxUltimateGauge = maxUltimateGauge;
    }

    public void SetValues(SOPlayer soPlayer)
    {
        base.SetValues(soPlayer.objectType, soPlayer.level, soPlayer.health, soPlayer.maxHealth, soPlayer.attack, soPlayer.defense, soPlayer.criticalChance, soPlayer.movementSpeed, soPlayer.experience, soPlayer.money ,soPlayer.mana, soPlayer.maxMana);
        this.luck = soPlayer.luck;
        this.maxExperience = soPlayer.maxExperience;
        this.ultimateGauge = soPlayer.ultimateGauge;
        this.maxUltimateGauge = soPlayer.maxUltimateGauge;
    }
    public void KillMonster(float experience, int money, float ultimateGauge)
    {
        AddExp(experience);
        AddMoney(money);
        this.ultimateGauge += ultimateGauge;
        if (ultimateGauge > maxUltimateGauge)
        {
            this.ultimateGauge = maxUltimateGauge;
        }
    }
    public void SetUltimateGauge(float num)
    {
        ultimateGauge = num;
    }

    
    public void SetMaxExperience(float MaxExperience)
    {
        this.maxExperience = MaxExperience;
    }
    public void AddMaxExperience(float MaxExperience)
    {
        this.maxExperience += MaxExperience;
    }


    public override void ShowInfo()
    {
        base.ShowInfo();
        Debug.Log(this.luck);
        Debug.Log(this.experience);
        Debug.Log(this.maxExperience);
        Debug.Log(this.ultimateGauge);
        Debug.Log(this.maxUltimateGauge);
    }
}
