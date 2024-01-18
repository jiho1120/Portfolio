using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckEnemy : Node
{
    Boss owner;

    public CheckEnemy(Boss owner)
    {
        this.owner = owner;
    }
    public override AllEnum.NodeState Evaluate()
    {
        throw new System.NotImplementedException();
    }
    
}
