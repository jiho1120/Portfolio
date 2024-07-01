using System.Collections.Generic;
using UnityEngine;

public class Love : PassiveSkill
{
    List<Creature> debuffedcreatures = new List<Creature>();

    public override void Deactivate()
    {
        base.Deactivate();
        foreach (var creature in debuffedcreatures)
        {
            creature.StopDecreaseAttCor();
        }
        debuffedcreatures.Clear();
    }


    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        Creature creature = other.GetComponent<Creature>();

        if (creature != null)
        {
            debuffedcreatures.Add(creature);
            creature.StartDecreaseAttCor(skilldata.effect, skilldata.cool);
        }
    }

    void OnTriggerExit(Collider other)
    {
        base.OnTriggerEnter(other);
        Creature creature = other.GetComponent<Creature>()
            ; creature.StopDecreaseAttCor();
        debuffedcreatures.Remove(creature);

    }

}
