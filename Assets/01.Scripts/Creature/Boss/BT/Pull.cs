using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pull : Node
{
    public Pull(Boss owner)
    {
        this.owner = owner;
    }
    public override AllEnum.NodeState Evaluate()
    {
        if (owner.IsPull)
        {
            owner.Pull();
            owner.NowState = AllEnum.StateEnum.Pull;
            return AllEnum.NodeState.Success;
        }
        else 
        {

            owner.StopPull();
            return AllEnum.NodeState.Failure;
        }
    }

    
}
