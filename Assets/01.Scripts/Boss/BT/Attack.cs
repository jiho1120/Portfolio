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
    public override AllEnum.NodeState Evaluate() // 사거리 2
    {
        if (owner.CheckDistance() < 4) // 스킬 사거리
        {
            owner.NowState = AllEnum.StateEnum.BasicAttack;
            return AllEnum.NodeState.Success;
        }
            return AllEnum.NodeState.Failure;

    }

}
