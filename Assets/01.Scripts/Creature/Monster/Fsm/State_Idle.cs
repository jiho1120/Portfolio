public class State_Idle : State
{
    public State_Idle(Monster monster, SetStateDel StateDel) : base(monster, StateDel)
    {
    }

    public override void OnStateEnter()
    {
        monster.Idle();
    }

    public override void OnStateExit()
    {

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
            if (monster.IsPull)
            {
                StateDel(AllEnum.States.Pull);
                return;
            }
            if (monster.IsKnockback)
            {
                StateDel(AllEnum.States.Knockback);
                return;
            }

            float dis = monster.CheckDir().sqrMagnitude;
            if (dis <= monster.attackDistance && monster.isAttackable)
            {
                StateDel(AllEnum.States.Attack);
                return;
            }
            if (monster.isHit)
            {
                StateDel(AllEnum.States.Hit);
                return;
            }
            if (dis > monster.attackDistance)
            {
                StateDel(AllEnum.States.Walk);
                return;
            }
        }
    }
}
