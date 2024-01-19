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
        Debug.Log("목적지 : "+GameManager.Instance.player.transform.position);
        Debug.Log("오너 속력 " +owner.agent.velocity);
        if (owner.CheckDistance() < 16)
        {
            owner.agent.isStopped = false;
            owner.agent.SetDestination(GameManager.Instance.player.transform.position);
            owner.SetMoveAnim(1f, owner.agent.velocity.z, owner.agent.velocity.x);
            owner.NowState = AllEnum.StateEnum.Walk;
            return AllEnum.NodeState.Success;
        }
        else //if (owner.CheckDistance() >= 16)
        {
            owner.agent.isStopped = false;
            owner.agent.SetDestination(GameManager.Instance.player.transform.position);
            owner.SetMoveAnim(1.5f,owner.agent.velocity.z, owner.agent.velocity.x);
            owner.NowState = AllEnum.StateEnum.Run;
            return AllEnum.NodeState.Success;
        }
        //else
        //{
        //    return AllEnum.NodeState.Failure;

        //}
    }
    
}
