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
        if (owner.isStop)
        {
            owner.Stop();
            return AllEnum.NodeState.Success;
        }
        return AllEnum.NodeState.Failure;
    }
}
