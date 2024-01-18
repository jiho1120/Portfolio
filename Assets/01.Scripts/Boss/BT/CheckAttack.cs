using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckAttack : Node

{
    Boss owner;

    public CheckAttack(Boss owner)
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
