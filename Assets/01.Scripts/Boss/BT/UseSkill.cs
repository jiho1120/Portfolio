using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseSkill : Node
{
    Boss owner;

    public UseSkill(Boss owner)
    {
        this.owner = owner;
    }
    public override AllEnum.NodeState Evaluate()
    {
        throw new System.NotImplementedException();
    }
    
}
