public class State_Die : State
{
    public State_Die(Monster monster, SetStateDel StateDel) : base(monster, StateDel)
    {
    }

    public override void OnStateEnter()
    {
        // 아이템을 죽으면서 떨굼 이게 싫으면 exit하면서 떨구면됨
        monster.Die();
    }

    public override void OnStateExit()
    {
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
