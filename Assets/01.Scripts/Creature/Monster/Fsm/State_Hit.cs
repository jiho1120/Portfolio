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
        monster.SetIsHit(false);
    }

    public override void OnStateStay()
    {
        float dis = monster.CheckDir().sqrMagnitude;

        if (monster.isDeActive)
        {
            StateDel(AllEnum.States.DeActivate);
            return;
        }
        else if (monster.isDead)
        {
            StateDel(AllEnum.States.Die);
            return;
        }
        else
        {
            if (dis <= monster.attackDistance)
            {
                if (monster.isAttack)
                {
                    StateDel(AllEnum.States.Attack);
                    return;
                }
                else
                {
                    StateDel(AllEnum.States.Idle);
                    return;
                }

            }
            else if (dis > monster.attackDistance)
            {
                StateDel(AllEnum.States.Walk);
                return;
            }
        }
    }
}
