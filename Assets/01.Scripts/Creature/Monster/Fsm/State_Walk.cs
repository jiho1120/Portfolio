using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;


public class State_Walk : State
{
    public State_Walk(Monster monster, SetStateDel StateDel) : base(monster, StateDel)
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
