using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class AirSlash : ActiveSkill
{
    private HashSet<int> hitMonsters = new HashSet<int>();
    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        Creature cre = other.GetComponent<Creature>();
        if (cre != null && !hitMonsters.Contains(cre.id))
        {
            Attack(cre);
        }
    }
}
