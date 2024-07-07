using System.Collections.Generic;
using UnityEngine;

public class MonStateMachine : MonoBehaviour
{
    public Monster owner;
    Dictionary<AllEnum.States, State> StateDic = new Dictionary<AllEnum.States, State>();
    AllEnum.States ExState = AllEnum.States.End; //이전상태 체크위함

    void Update()
    {
        if (ExState == owner.NowState && owner.NowState != AllEnum.States.End)
        {
            StateDic[owner.NowState].OnStateStay();
        }
    }
    public void Init()
    {
        owner = GetComponent<Monster>();
        StateDic.Add(AllEnum.States.Idle, new State_Idle(owner, SetState));
        StateDic.Add(AllEnum.States.Walk, new State_Walk(owner, SetState));
        StateDic.Add(AllEnum.States.Attack, new State_Attack(owner, SetState));
        StateDic.Add(AllEnum.States.Hit, new State_Hit(owner, SetState));
        StateDic.Add(AllEnum.States.Knockback, new State_Knockback(owner, SetState));
        StateDic.Add(AllEnum.States.Pull, new State_Pull(owner, SetState));
        StateDic.Add(AllEnum.States.Die, new State_Die(owner, SetState));
        StateDic.Add(AllEnum.States.DeActivate, new State_DeActivate(owner, SetState));
    }
    public void StopNowState()
    {
        owner.NowState = AllEnum.States.Die;
        if (ExState != AllEnum.States.End)
        {
            StateDic[ExState].OnStateExit();
        }
        ExState = AllEnum.States.End;
    }
    public void SetState(AllEnum.States _enum)
    {
        owner.NowState = _enum;
        if (ExState != owner.NowState)
        {
            if (ExState != AllEnum.States.End)
                StateDic[ExState].OnStateExit();

            StateDic[owner.NowState].OnStateEnter();
            ExState = owner.NowState;
        }
    }
}
