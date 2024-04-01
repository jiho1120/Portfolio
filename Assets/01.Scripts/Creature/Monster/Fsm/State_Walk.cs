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
        float dis = monster.CheckDir().sqrMagnitude;

        monster.Move(GameManager.Instance.player.transform.position);

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
        }
    }
}
