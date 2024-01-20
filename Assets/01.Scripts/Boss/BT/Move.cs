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
        if (!owner.isStop)
        {
            owner.SetAgentDirection(owner.agent.destination);
            if (owner.CheckDistance() > 100)
            {
                owner.agent.isStopped = false;
                owner.agent.SetDestination(GameManager.Instance.player.transform.position);
                owner.SetMoveAnim(1f, owner.agent.velocity.z, owner.agent.velocity.x);
                owner.NowState = AllEnum.StateEnum.Run;
                return AllEnum.NodeState.Success;
                
            }
            else if (owner.CheckDistance() < 4)
            {
                owner.agent.isStopped = true;
                owner.SetMoveAnim(0, owner.agent.velocity.z, owner.agent.velocity.x);
                owner.NowState = AllEnum.StateEnum.Idle;
                return AllEnum.NodeState.Success;
            }
            else/* if (owner.CheckDistance() >= 49)*/
            {
                owner.agent.isStopped = false;
                owner.agent.SetDestination(GameManager.Instance.player.transform.position);
                owner.SetMoveAnim(0.5f, owner.agent.velocity.z, owner.agent.velocity.x);
                owner.NowState = AllEnum.StateEnum.Walk;
                return AllEnum.NodeState.Success;
            }
        }
        else
        {
            return AllEnum.NodeState.Failure;

        }
    }
    
}
