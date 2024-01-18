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
        if (owner.IsDead())
        {
            owner.agent.isStopped = true;
            owner.gameObject.SetActive(false);
            return AllEnum.NodeState.Success;
        }
        return AllEnum.NodeState.Failure;
    }
    
}
