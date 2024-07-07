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
        // �׳� ���ԵǸ� ��ų�� ���׷��̵� �Ǿ�����(10 -> 20) �� ��ġ�� �����Ե� �׷��� ���ݷ��� �׻� �����Ϳ��� �ҷ�����
        cre.GetAttToData();
    }

}