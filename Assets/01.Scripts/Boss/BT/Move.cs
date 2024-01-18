using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : Node
{
    Boss owner;

    public Move(Boss owner)
    {
        this.owner = owner;
    }
    public override AllEnum.NodeState Evaluate()
    {
        if (owner.speed == 1f)
        {
            owner.agent.isStopped = false;
            owner.agent.SetDestination(GameManager.Instance.player.transform.position);
            owner.SetMoveAnim(owner.agent.velocity.z, owner.agent.velocity.x);
            return AllEnum.NodeState.Success;
        }
        else if (owner.speed == 1.5f)
        {
            owner.agent.isStopped = false;
            owner.agent.SetDestination(GameManager.Instance.player.transform.position);
            owner.SetMoveAnim(owner.agent.velocity.z, owner.agent.velocity.x);
            return AllEnum.NodeState.Success;
        }
        else
        {
            return AllEnum.NodeState.Failure;

        }
    }
    
}
