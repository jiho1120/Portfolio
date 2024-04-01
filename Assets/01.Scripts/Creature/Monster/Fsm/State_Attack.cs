public class State_Attack : State
{
    public State_Attack(Monster monster, SetStateDel StateDel) : base(monster, StateDel)
    {
    }

    public override void OnStateEnter()
    {
        monster.transform.LookAt(GameManager.Instance.player.transform.position);
        monster.Agent.isStopped = true;
        monster.SetAttackAnim();


    }

    public override void OnStateExit()
    {
    }

    public override void OnStateStay()
    {
        float dis = monster.CheckDir().sqrMagnitude;

        if (monster.isDeActive)
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
            if (monster.isHit)
            {
                StateDel(AllEnum.States.Hit);
                return;
            }
            else if (dis <= monster.attackDistance)
            {
                StateDel(AllEnum.States.Idle);
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
