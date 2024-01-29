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
        monster.dir = monster.CheckDir();
        monster.SetAttackState();

        if (monster.IsDead())
        {
            StateDel(AllEnum.States.Die);
            return;
        }
        else
        {
            if (monster.CheckDir().sqrMagnitude <= 4f)
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
            else if (monster.dir.sqrMagnitude > 4f)
            {
                StateDel(AllEnum.States.Walk);
                return;
            }
        }
    }
}
