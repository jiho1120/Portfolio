using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_Attack : State
{
    public State_Attack(Monster monster, SetStateDel StateDel) : base(monster, StateDel)
    {
    }

    public override void OnStateEnter()
    {
        monster.Attack();
    }

    public override void OnStateExit()
    {
        monster.isAttack = false;
    }

    public override void OnStateStay()
    {
        if (monster.isDead)
        {
            StateDel(AllEnum.States.Die);
            return;
        }
        else
        {
            if (monster.CheckDir().sqrMagnitude <= 4f)
            {
                StateDel(AllEnum.States.Idle);
                return;
            }
            if (monster.CheckDir().sqrMagnitude > 4f)
            {
                StateDel(AllEnum.States.Walk);
                return;
            }
            if (monster.isHit)
            {
                StateDel(AllEnum.States.Hit);
                return;
            }
        }
    }
}
