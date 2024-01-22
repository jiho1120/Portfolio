using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_Die : State
{
    public State_Die(Monster monster, SetStateDel StateDel) : base(monster, StateDel)
    {
    }

    public override void OnStateEnter()
    {
        Debug.Log(monster.gameObject.name + "Á×À½ »óÅÂ µé¾î¿È");        
        monster.Dead(false);
    }

    public override void OnStateExit()
    {
    }

    public override void OnStateStay()
    {
    }
}
