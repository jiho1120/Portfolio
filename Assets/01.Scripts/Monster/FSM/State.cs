using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    //������ �� ���� ������ ������ �־�� ��.
    protected Monster monster; //�ൿ ����.
    public delegate void SetStateDel(AllEnum.States _enum);
    protected SetStateDel StateDel;

    public State(Monster monster, SetStateDel StateDel)
    {
        this.monster = monster;
        this.StateDel = StateDel;
    }

    public abstract void OnStateEnter(); //�� ���¿� ó�� �������� �����ؾ��ϴ°�
    public abstract void OnStateStay(); //�� ���¸� �����Ѵٸ� �ؾ��ϴ� ��
    public abstract void OnStateExit(); //�� ���¸� ������ �����ؾ��� ��.
}