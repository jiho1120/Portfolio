using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Die : Node
{
    Boss owner;

    public Die(Boss owner)
    {
        this.owner = owner;
    }

    public override AllEnum.NodeState Evaluate()
    {
        if (owner.bossStat.health <= 0)
        {
            owner.Dead(false);
            owner.NowState = AllEnum.StateEnum.DIe;
            return AllEnum.NodeState.Success;
        }
        return AllEnum.NodeState.Failure;
    }
    
}
