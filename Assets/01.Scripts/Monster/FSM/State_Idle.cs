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
        monster.dir = monster.CheckDir();

        if (monster.isDead)
        {
            StateDel(AllEnum.States.Die);
            return;
        }
        else
        {
            monster.SetAttackState();
            if (monster.isAttack) // ������ �����Ÿ� ���̶� �Ÿ�üũ ���ص���
            {
                StateDel(AllEnum.States.Attack);
                return;
            }
            else if (monster.isHit)
            {
                StateDel(AllEnum.States.Hit);
                return;
            }
            else if (monster.dir.sqrMagnitude > 4f)
            {
                StateDel(AllEnum.States.Walk);
                return;
            }
        }
    }
}
