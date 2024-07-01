public class Heal : PassiveSkill
{
    Creature cre;

    public override void Activate()
    {
        base.Activate();
        cre = GetComponentInParent<Creature>();
        cre.StartSetHPCor(skilldata.effect, skilldata.cool);
    }
    public override void Deactivate()
    {
        base.Deactivate();
        cre = GetComponentInParent<Creature>();
        cre.StopSetHpCor();
    }
}
