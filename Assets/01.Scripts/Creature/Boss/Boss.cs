public class Boss : UseSKillCharacter
{
    public override void Activate()
    {
        base.Activate();
    }

    public override void Deactivate()
    {
        base.Deactivate();
    }

    public override void Die()
    {
        throw new System.NotImplementedException();
    }

    public void Init()
    {
        throw new System.NotImplementedException();
    }

    public override void implementTakeDamage()
    {
        throw new System.NotImplementedException();
    }
    public override void GetAttToData()
    {
        Stat.attack = DataManager.Instance.gameData.bossData.bossStat.attack;
    }
}
