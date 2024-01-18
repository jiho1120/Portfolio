using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : Node
{
    Boss owner;

    public Attack(Boss owner)
    {
        this.owner = owner;
    }
    public override AllEnum.NodeState Evaluate() // »ç°Å¸® 2
    {
        throw new System.NotImplementedException();
    }
    
}
