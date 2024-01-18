using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle : Node
{
    Boss owner;

    public Idle(Boss owner)
    {
        this.owner = owner;
    }
    public override AllEnum.NodeState Evaluate()
    {
        owner.agent.isStopped = true;
        owner.agent.velocity = Vector3.zero;
        owner.SetMoveAnim(owner.agent.velocity.z, owner.agent.velocity.x);
        return AllEnum.NodeState.Success;
    }
    
}
