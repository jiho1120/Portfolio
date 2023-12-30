using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_Walk : State
{

    Vector3 goalPos = Vector3.zero;

    public State_Walk(Monster monster, SetStateDel StateDel) : base(monster, StateDel)
    {
    }

    public override void OnStateEnter()
    {
        //������ �������� �� ��Ű��
        monster.Move(goalPos); //�׺� ������ �Ѱ��̰�
    }

    public override void OnStateExit()
    {
    }

    public override void OnStateStay()
    {
        monster.SetMoveAnim();
    }
}
