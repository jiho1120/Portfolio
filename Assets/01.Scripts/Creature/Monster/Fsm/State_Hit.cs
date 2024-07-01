public class State_Hit : State
{
    public State_Hit(Monster monster, SetStateDel StateDel) : base(monster, StateDel)
    {
    }

    public override void OnStateEnter()
    {
        monster.Hit();
    }

    public override void OnStateExit()
    {
        monster.SetIsHit(false);
    }

    public override void OnStateStay()
    {

        if (monster.isDeActive || !GameManager.Instance.stageStart)
        {
            StateDel(AllEnum.States.DeActivate);
            return;
        }
        else if (monster.isDead)
        {
            StateDel(AllEnum.States.Die);
            return;
        }
        else
        {
            if (monster.IsKnockback)
            {
                StateDel(AllEnum.States.Knockback);
                return;
            }
            float dis = monster.CheckDir().sqrMagnitude;
            if (dis <= monster.attackDistance)
            {
                if (monster.isAttack)
                {
                    StateDel(AllEnum.States.Attack);
                    return;
                }
                else
                {
                    StateDel(AllEnum.States.Idle);
                    return;
                }

            }
            if (dis > monster.attackDistance)
            {
                StateDel(AllEnum.States.Walk);
                return;
            }
        }
    }
}
