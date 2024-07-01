public class State_Walk : State
{
    public State_Walk(Monster monster, SetStateDel StateDel) : base(monster, StateDel)
    {
    }

    public override void OnStateEnter()
    {
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
        }
        monster.Move(GameManager.Instance.player.transform.position);
    }
}
