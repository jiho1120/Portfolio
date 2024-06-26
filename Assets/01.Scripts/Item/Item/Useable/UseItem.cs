using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UseItem : Item, IUsable
{
    public virtual void Use()
    {
        throw new System.NotImplementedException();
    }

    
}
