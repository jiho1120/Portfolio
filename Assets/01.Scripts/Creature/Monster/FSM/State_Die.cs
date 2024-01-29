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
        //Debug.Log(monster.gameObject.name + "���� ���� ����");  
        force = monster.force; // ���⼭ force���� �ޱ�
        if (force) // �׾��ִ� �ֵ��� �״� �ð����� ���ֱ�
        {
            monster.StopDieCor();
            if (monster.monStateMachine != null)
            {
                monster.monStateMachine.StopNowState();
            }

            if (!monster.IsDead())// ����ִ� �ֵ��� ������ ����
            {
                monster.SetDead(true);
                monster.SetDeadAnim();
            }
            MonsterManager.Instance.MonsterPool().ReturnObjectToPool(monster);
        }
        else
        {
            //����ִٰ� �״� �Ŵϱ�. ������ �����Ǿ�����???
            monster.SetDead(true);// �̹� �̻���
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
