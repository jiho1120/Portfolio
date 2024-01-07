using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

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
        monster.dir = monster.CheckDir();
        monster.SetAttackState();
        monster.Move(GameManager.Instance.player.transform.position);

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
        }
    }
}
