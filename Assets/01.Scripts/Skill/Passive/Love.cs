using System.Collections.Generic;
using UnityEngine;

public class Love : PassiveSkill
{
    private HashSet<Creature> hitMonsters = new HashSet<Creature>();

    public override void Deactivate()
    {
        base.Deactivate();
        foreach (var creature in hitMonsters)
        {
            creature.StopDecreaseAttCor();
        }
        hitMonsters.Clear();
    }


    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        Creature creature = other.GetComponent<Creature>();

        if (creature != null)
        {
            hitMonsters.Add(creature);
            creature.StartDecreaseAttCor(skilldata.effect, skilldata.cool);
        }
    }

    void OnTriggerExit(Collider other)
    {
        base.OnTriggerEnter(other);
        Creature creature = other.GetComponent<Creature>()
            ; creature.StopDecreaseAttCor();
        hitMonsters.Remove(creature);
    }

}
