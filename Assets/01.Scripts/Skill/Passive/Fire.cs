public class Fire : PassiveSkill
{
    Creature cre;

    public override void Activate()
    {
        base.Activate();
        cre = GetComponentInParent<Creature>();
        cre.SetAtt(cre.Stat.attack + skilldata.effect);

    }
    
    public override void Deactivate()
    {
        base.Deactivate();
        cre = GetComponentInParent<Creature>();
        // 그냥 빼게되면 스킬이 업그레이드 되었을때(10 -> 20) 더 수치가 빠지게됨 그래서 공격력은 항상 데이터에서 불러오기
        cre.GetAttToData();
    }

}