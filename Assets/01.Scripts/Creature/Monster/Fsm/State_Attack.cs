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
            if (monster.isHit)
            {
                StateDel(AllEnum.States.Hit);
                return;
            }
            float dis = monster.CheckDir().sqrMagnitude;
            if (dis <= monster.attackDistance)
            {
                StateDel(AllEnum.States.Idle);
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
