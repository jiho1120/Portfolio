using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_Walk : State
{

    Vector3 goalPos = Vector3.zero;

    public State_Walk(Monster monster, SetStateDel StateDel) : base(monster, StateDel)
    {
    }

    public override void OnStateEnter()
    {
        //실제로 움직여라 도 시키기
        monster.Move(goalPos); //네비 세팅을 한것이고
    }

    public override void OnStateExit()
    {
    }

    public override void OnStateStay()
    {
        monster.SetMoveAnim();
    }
}
