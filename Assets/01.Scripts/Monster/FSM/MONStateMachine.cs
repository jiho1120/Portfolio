using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//상태를 판별해서 다음상태로 바꿔줌.
public class MONStateMachine : MonoBehaviour 
{
    public Monster owner;
    public Transform TargetTr; //상대방의 transform
    Dictionary<AllEnum.States, State> StateDic = new Dictionary<AllEnum.States, State>();
    AllEnum.States ExState = AllEnum.States.End; //이전상태 체크위함
    AllEnum.States NowState = AllEnum.States.Idle; //현재상태 체크위함
    private void Start()
    {
        
    }
    private void Update()
    {
        if (ExState == owner.NowState
            && owner.NowState != AllEnum.States.End 
            //2번, nowState가 End상태면 일을 안하도록 한다.
            )
        {
            StateDic[owner.NowState].OnStateStay();
        }
    }
    public void SetInit()
    {
        owner = GetComponent<Monster>();
        StateDic.Add(AllEnum.States.Idle, new State_Idle(owner, SetState));
        StateDic.Add(AllEnum.States.Walk, new State_Walk(owner, SetState));
        StateDic.Add(AllEnum.States.Attack, new State_Attack(owner, SetState));
        StateDic.Add(AllEnum.States.Hit, new State_Hit(owner, SetState));
        StateDic.Add(AllEnum.States.Die, new State_Die(owner, SetState));

        SetState(AllEnum.States.Walk);
    }

    public void SetState(AllEnum.States _enum)
    {
        NowState = _enum;
        if (ExState != NowState)
        {
            if (ExState != AllEnum.States.End)
                StateDic[ExState].OnStateExit();

            StateDic[NowState].OnStateEnter();
            ExState = NowState;
        }
    }
    
}
