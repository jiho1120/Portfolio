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
        // 목표 지점까지의 실제 거리
        owner.ActualDistance = Mathf.Sqrt(owner.CheckDir().sqrMagnitude);
        if (owner.ActualDistance <= 4) // 평타 사거리
        {
            owner.BasicAttack();
            owner.NowState = AllEnum.StateEnum.BasicAttack;
            return AllEnum.NodeState.Success;
        }
        return AllEnum.NodeState.Failure;

    }

}
