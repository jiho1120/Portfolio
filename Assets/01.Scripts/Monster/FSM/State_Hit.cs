using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_Hit : State
{
    public State_Hit(Monster monster, SetStateDel StateDel) : base(monster, StateDel)
    {
    }

    public override void OnStateEnter()
    {
        monster.Hit();
    }

    public override void OnStateExit()
    {
        monster.isHit = false;
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
            monster.SetAttackState();
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
            if (monster.isAttack)
            {
                StateDel(AllEnum.States.Hit);
                return;
            }
        }

            

        
    }
}
