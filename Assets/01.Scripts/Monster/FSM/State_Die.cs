using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_Die : State
{
    Coroutine dieCor = null;
    bool force = false;

    public State_Die(Monster monster, SetStateDel StateDel) : base(monster, StateDel)
    {
    }

    public override void OnStateEnter()
    {
        Debug.Log(monster.gameObject.name + "���� ���� ����");  
        force = monster.force; // ���⼭ force���� �ޱ�
        if (force) // �׾��ִ� �ֵ��� �״� �ð����� ���ֱ�
        {
            monster.StopDieCor();
            if (monster.monStateMachine != null)
                monster.monStateMachine.StopNowState();

            if (monster.isDead == false)// ����ִ� �ֵ��� ������ ����
            {
                monster.isDead = true;
                monster.SetDeadAnim();
            }
            MonsterManager.Instance.MonsterPool().ReturnObjectToPool(monster);
        }
        else
        {
            //����ִٰ� �״� �Ŵϱ�. ������ �����Ǿ�����???
            monster.isDead = true;//�̹� �̻���.
            monster.SetDeadAnim();
            GameManager.Instance.player.playerStat.KillMonster(monster.monsterStat.experience, monster.monsterStat.money, 10); // ���� ���������� �ñر� 10�� 
            GameManager.Instance.killMonster++;
            monster.DropRandomItem();

            if (dieCor == null)
            {
                monster.StartDieCor();
            }
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
