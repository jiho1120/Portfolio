using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stun : Node
{
    Boss owner;

    public Stun(Boss owner)
    {
        this.owner = owner;
    }
    public override AllEnum.NodeState Evaluate()
    {
        if (owner.isStop)
        {
            owner.Stop();
            owner.NowState = AllEnum.StateEnum.Stun;
            return AllEnum.NodeState.Success;
        }
        
        return AllEnum.NodeState.Failure;
    }
}
