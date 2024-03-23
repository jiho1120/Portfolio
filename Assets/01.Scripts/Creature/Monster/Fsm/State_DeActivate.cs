using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_DeActivate : State
{
    public State_DeActivate(Monster monster, SetStateDel StateDel) : base(monster, StateDel)
    {
    }

    public override void OnStateEnter()
    {
        throw new System.NotImplementedException();
    }

    public override void OnStateExit()
    {
        throw new System.NotImplementedException();
    }

    public override void OnStateStay()
    {
        throw new System.NotImplementedException();
    }
}
