using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_Die : State
{
    public State_Die(Monster monster, SetStateDel StateDel) : base(monster, StateDel)
    {
    }

    public override void OnStateEnter()
    {
        monster.Agent.isStopped = true;
        monster.rb.isKinematic = true;
        monster.Die();

    }

    public override void OnStateExit()
    {
    }

    public override void OnStateStay()
    {
        monster.Agent.isStopped = true;
        monster.rb.isKinematic = true; // true ���� �ٸ� ���� ������ �ȹ���
        if (monster.isDeActive)
        {
            StateDel(AllEnum.States.DeActivate);
            return;
        }
    }
}
