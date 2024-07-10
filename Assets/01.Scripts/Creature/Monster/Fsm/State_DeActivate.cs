public class State_DeActivate : State
{
    public State_DeActivate(Monster monster, SetStateDel StateDel) : base(monster, StateDel)
    {
    }

    public override void OnStateEnter()
    {
        monster.Deactivate();
    }

    public override void OnStateExit() 
    {

    }

    public override void OnStateStay()
    {
        
    }
}
