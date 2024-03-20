[System.Serializable]
public class GameData
{
    public PlayerData playerData = new PlayerData();
    public MonsterData monsterData = new MonsterData();
    public BossData bossData = new BossData();

    public void SetGameData()
    {
        playerData.playerStat.name = NewUIManager.Instance.newPlayerName.text; // �Է��� �̸��� �����ؿ�
        playerData.playerStat.SetStat(DataManager.Instance.SOPlayerStat);
        monsterData.monsterStat.SetStat(DataManager.Instance.SOMonsterStat);
        bossData.bossStat.SetStat(DataManager.Instance.SOBossStat);
    }

}

[System.Serializable]
public class StatData
{
    // �̸�, ����, ����, �������� ����
    public string name;
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

    public void SetStat(SOStat SO)
    {
        objectType = SO.objectType;
        level = SO.level;
        health = SO.health;
        maxHealth = SO.maxHealth;
        attack = SO.attack;
        defense = SO.defense;
        criticalChance = SO.criticalChance;
        movementSpeed = SO.movementSpeed;
        experience = SO.experience;
        money = SO.money;
        mana = SO.mana;
        maxMana = SO.maxMana;
        luck = SO.luck;
        maxExperience = SO.maxExperience;
        ultimateGauge = SO.ultimateGauge;
        maxUltimateGauge = SO.ultimateGauge;
    }
}
[System.Serializable]
public class PlayerData
{
    public StatData playerStat = new StatData();
}

[System.Serializable]
public class MonsterData
{
    public StatData monsterStat = new StatData();
}

[System.Serializable]
public class BossData
{
    public StatData bossStat = new StatData();
}
