using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_DeActivate : State
{
    public State_DeActivate(Monster monster, SetStateDel StateDel) : base(monster, StateDel)
    {
    }

    public override void OnStateEnter()
    {
        monster.rb.isKinematic = true;
        monster.Agent.isStopped = true;
        monster.SetIsAttack(false);
        monster.SetIsHit(false);
        monster.SetIsDead(true); // �̰� �������� ���� �ɸ� �׷��� true���� �������
        monster.SetIsDeActive(true);
        monster.gameObject.SetActive(false);
    }

    public override void OnStateExit() // Ȱ��ȭ �ɶ�
    {
        monster.gameObject.SetActive(true);
        monster.rb.isKinematic = false;
        monster.Agent.isStopped = false;
        monster.SetIsAttack(true);
        monster.SetIsHit(false);
        monster.SetIsDead(false);
        monster.SetIsDeActive(false);
    }

    public override void OnStateStay()
    {
    }
}
