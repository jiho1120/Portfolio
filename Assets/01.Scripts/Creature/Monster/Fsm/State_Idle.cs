using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_Idle : State
{
    public State_Idle(Monster monster, SetStateDel StateDel) : base(monster, StateDel)
    {
    }

    public override void OnStateEnter()
    {
        monster.Idle();
    }

    public override void OnStateExit()
    {

    }

    public override void OnStateStay()
    {
        float dis = monster.CheckDir().sqrMagnitude;

        if (monster.isDeActive)
        {
            StateDel(AllEnum.States.DeActivate);
            return;
        }
        else if(monster.isDead)
        {
            StateDel(AllEnum.States.Die);
            return;
        }
        else
        {
            if (dis <= monster.attackDistance && monster.isAttack) 
            {
                StateDel(AllEnum.States.Attack);
                return;
            }
            else if (monster.isHit)
            {
                StateDel(AllEnum.States.Hit);
                return;
            }
            else if (dis > monster.attackDistance)
            {
                StateDel(AllEnum.States.Walk);
                return;
            }
        }
    }
}
