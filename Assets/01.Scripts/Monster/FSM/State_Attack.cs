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
        monster.dir = monster.CheckDir();

        if (monster.isDead)
        {
            StateDel(AllEnum.States.Die);
            return;
        }
        else
        {
            if (monster.isHit)
            {
                StateDel(AllEnum.States.Hit);
                return;
            }
            else if (monster.dir.sqrMagnitude <= 4f)
            {
                StateDel(AllEnum.States.Idle);
                return;
                
            }
            else //맞지도 않고 공격거리 밖이면
            {
                StateDel(AllEnum.States.Walk);
                return;
            }
        }
    }
}
