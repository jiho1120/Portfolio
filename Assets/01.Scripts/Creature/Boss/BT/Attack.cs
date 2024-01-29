using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : Node
{
    Boss owner;

    public Attack(Boss owner)
    {
        this.owner = owner;
    }
    public override AllEnum.NodeState Evaluate() 
    {
        if (owner.CheckDistance() <= 4) // 평타 사거리
        {
            owner.BasicAttack();
            owner.NowState = AllEnum.StateEnum.BasicAttack;
            return AllEnum.NodeState.Success;
        }
        return AllEnum.NodeState.Failure;

    }

}
