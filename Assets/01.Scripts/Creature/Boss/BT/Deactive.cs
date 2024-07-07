using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deactive : Node
{
    public Deactive(Boss owner)
    {
        this.owner = owner;
    }
    public override AllEnum.NodeState Evaluate()
    {
        if (this.owner.isDeActive)
        {
            owner.Deactivate();
            owner.NowState = AllEnum.StateEnum.DeActive;
            return AllEnum.NodeState.Success;
        }
        return AllEnum.NodeState.Failure;
    }

    
}
