using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : Node
{
    
    public Move(Boss owner)
    {
        this.owner = owner;
    }
    public override AllEnum.NodeState Evaluate()
    {
        owner.LookPlayer();
        owner.ActualDistance = Mathf.Sqrt(owner.CheckDir().sqrMagnitude);

        if (owner.ActualDistance > 10)
        {
            owner.agent.isStopped = false;
            owner.agent.SetDestination(GameManager.Instance.player.transform.position);
            owner.SetMoveAnim(1f, owner.agent.velocity.z, owner.agent.velocity.x);

            owner.NowState = AllEnum.StateEnum.Run;
            return AllEnum.NodeState.Success;

        }
        else if (owner.ActualDistance < 2)
        {
            owner.agent.isStopped = true;
            owner.agent.ResetPath();
            owner.SetMoveAnim(0, owner.agent.velocity.z, owner.agent.velocity.x);

            owner.NowState = AllEnum.StateEnum.Idle;
            return AllEnum.NodeState.Success;
        }
        else 
        {
            owner.agent.isStopped = false;
            owner.agent.SetDestination(GameManager.Instance.player.transform.position);
            owner.SetMoveAnim(0.5f, owner.agent.velocity.z, owner.agent.velocity.x);
            owner.NowState = AllEnum.StateEnum.Walk;
            return AllEnum.NodeState.Success;
        }
        
    }

}
