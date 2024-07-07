using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : Node
{
    public Attack(Boss owner)
    {
        this.owner = owner;
    }
    public override AllEnum.NodeState Evaluate() 
    {
        // ��ǥ ���������� ���� �Ÿ�
        owner.ActualDistance = Mathf.Sqrt(owner.CheckDir().sqrMagnitude);
        if (owner.ActualDistance <= 4) // ��Ÿ ��Ÿ�
        {
            owner.BasicAttack();
            owner.NowState = AllEnum.StateEnum.BasicAttack;
            return AllEnum.NodeState.Success;
        }
        return AllEnum.NodeState.Failure;

    }

}
