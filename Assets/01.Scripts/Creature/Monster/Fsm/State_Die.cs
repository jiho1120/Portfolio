public class State_Die : State
{
    public State_Die(Monster monster, SetStateDel StateDel) : base(monster, StateDel)
    {
    }

    public override void OnStateEnter()
    {
        monster.Die();
    }

    public override void OnStateExit()
    {
        monster.DropRandomItem();
    }

    public override void OnStateStay()
    {
        monster.Agent.isStopped = true;
        monster.rb.isKinematic = true; // true 여야 다른 힘에 영향을 안받음
        if (monster.isDeActive || !GameManager.Instance.stageStart)
        {
            StateDel(AllEnum.States.DeActivate);
            return;
        }
    }
}
