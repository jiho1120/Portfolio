public class State_Pull : State
{
    public State_Pull(Monster monster, SetStateDel StateDel) : base(monster, StateDel)
    {
    }

    public override void OnStateEnter()
    {
        monster.Pull();
    }

    public override void OnStateExit()
    {
        monster.StopPull();
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
        else if (monster.IsPull)
        {
            return;
        }
        else
        {
            StateDel(AllEnum.States.Idle);
            return;
        }
    }
}
