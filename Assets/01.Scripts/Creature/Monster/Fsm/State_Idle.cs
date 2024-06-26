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
            float dis = monster.CheckDir().sqrMagnitude;

            if (dis <= monster.attackDistance && monster.isAttack)
            {
                StateDel(AllEnum.States.Attack);
                return;
            }
            else if (monster.isHit)
            {
                StateDel(AllEnum.States.Hit);
                return;
            }
            else if (dis > monster.attackDistance)
            {
                StateDel(AllEnum.States.Walk);
                return;
            }
        }
    }
}
