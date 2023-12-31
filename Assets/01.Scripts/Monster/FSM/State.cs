using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    //소유주 에 대한 정보도 가지고 있어야 함.
    protected Monster monster; //행동 주인.
    public delegate void SetStateDel(AllEnum.States _enum);
    protected SetStateDel StateDel;

    public State(Monster monster, SetStateDel StateDel)
    {
        this.monster = monster;
        this.StateDel = StateDel;
    }

    public abstract void OnStateEnter(); //이 상태에 처음 들어왔을때 세팅해야하는것
    public abstract void OnStateStay(); //이 상태를 지속한다면 해야하는 일
    public abstract void OnStateExit(); //이 상태를 나갈때 정리해야할 것.
}