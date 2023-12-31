using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        monster.SetAttackState();
        if (monster.CheckDir().sqrMagnitude <= 4f)
        {
            StateDel(AllEnum.States.Idle);
            return;
        }
        if (monster.isHit)
        {
            StateDel(AllEnum.States.Hit);
            return;
        }
        if (monster.isDead)
        {
            StateDel(AllEnum.States.Die);
            return;
        }
        monster.Move(monster.TargetTr.position);
    }
}
