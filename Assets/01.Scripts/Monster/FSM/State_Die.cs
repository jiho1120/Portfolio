using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_Die : State
{
    bool force = false;

    public State_Die(Monster monster, SetStateDel StateDel) : base(monster, StateDel)
    {
    }

    public override void OnStateEnter()
    {
        //Debug.Log(monster.gameObject.name + "죽음 상태 들어옴");  
        force = monster.force; // 여기서 force인지 받기
        if (force) // 죽어있던 애들을 죽는 시간없이 없애기
        {
            monster.StopDieCor();
            if (monster.monStateMachine != null)
            {
                monster.monStateMachine.StopNowState();
            }

            if (monster.isDead == false)// 살아있는 애들을 강제로 죽임
            {
                monster.isDead = true;
                monster.SetDeadAnim();
            }
            MonsterManager.Instance.MonsterPool().ReturnObjectToPool(monster);
        }
        else
        {
            //살아있다가 죽는 거니까. 이전에 뭐가되어있음???
            monster.isDead = true;//이미 이상태.
            monster.SetDeadAnim();
            GameManager.Instance.player.CatchMonster(monster.monsterStat.experience, monster.monsterStat.money);
            GameManager.Instance.AddKillMonster();
            monster.DropRandomItem();

            if (monster.dieCor == null)
            {
                monster.StartDieCor();
            }
            UiManager.Instance.playerConditionUI.SetUI();
        }
    }

    public override void OnStateExit()
    {
    }

    public override void OnStateStay()
    {
        monster.Agent.isStopped = true;
    }
}
