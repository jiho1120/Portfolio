
using UnityEngine;

public class State_Knockback : State
{
    public State_Knockback(Monster monster, SetStateDel StateDel) : base(monster, StateDel)
    {
    }

    public override void OnStateEnter()
    {
        monster.Knockback();
    }

    public override void OnStateExit()
    {
        // 외부힘으로 안움직이게함
        monster.rb.isKinematic = true;
        monster.IsKnockback = false;

    }
    public override void OnStateStay()
    {

        if (monster.isDeActive || !GameManager.Instance.stageStart)
        {
            StateDel(AllEnum.States.DeActivate);
            return;
        }
        else if (monster.isDead)
        {
            StateDel(AllEnum.States.Die);
            return;
        }
        else if (monster.IsPull)
        {
            StateDel(AllEnum.States.Pull);
            return;
        }
        else if (monster.IsKnockback)
        {
            return;
        }
        else
        {
            StateDel(AllEnum.States.Idle);
            return;
        }
    }
}
