public class State_DeActivate : State
{
    public State_DeActivate(Monster monster, SetStateDel StateDel) : base(monster, StateDel)
    {
    }

    public override void OnStateEnter()
    {
        monster.Deactivate();
    }

    public override void OnStateExit() // 활성화 될때
    {
        monster.gameObject.SetActive(true);
        monster.rb.isKinematic = false;
        monster.Agent.isStopped = false;
        monster.SetIsAttack(true);
        monster.SetIsHit(false);
        
    }

    public override void OnStateStay()
    {
        if (!monster.isDeActive && !monster.isDead)
        {
            StateDel(AllEnum.States.Idle);
            return;
        }
    }
}
