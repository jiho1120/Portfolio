
using UnityEngine;

public class State_Knockback : State
{
    public State_Knockback(Monster monster, SetStateDel StateDel) : base(monster, StateDel)
    {
    }

    public override void OnStateEnter()
    {
        monster.rb.isKinematic = false;
        monster.Knockback();
    }

    public override void OnStateExit()
    {
        // 외부힘으로 안움직이게함
        monster.rb.isKinematic = true;
    }
    public override void OnStateStay()
    {
        if (monster.IsKnockback)
        {
            return;
        }
        else
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
            else
            {
                StateDel(AllEnum.States.Idle);
                return;
            }
        }
    }
}
