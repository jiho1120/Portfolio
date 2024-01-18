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
    public override AllEnum.NodeState Evaluate()
    {
        throw new System.NotImplementedException();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
