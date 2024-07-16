using System.Collections.Generic;
using UnityEngine;

public class MonStateMachine : MonoBehaviour
{
    public Monster owner;// 상태 기계가 제어하는 몬스터 객체
    Dictionary<AllEnum.States, State> StateDic = new Dictionary<AllEnum.States, State>(); // 상태와 상태 객체를 매핑하는 딕셔너리
    AllEnum.States ExState = AllEnum.States.End; //이전상태 체크위함

    void Update()
    {
        // 매 프레임마다 현재 상태를 유지하고 행동을 실행
        if (ExState == owner.NowState && owner.NowState != AllEnum.States.End)
        {
            StateDic[owner.NowState].OnStateStay();
        }
    }
    public void Init()
    {
        // 상태 기계 초기화
        owner = GetComponent<Monster>(); // 몬스터 객체 설정
        // 가능한 모든 상태를 상태 딕셔너리에 추가
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
        // 현재 상태를 멈추고 'Die' 상태로 설정
        owner.NowState = AllEnum.States.Die;
        if (ExState != AllEnum.States.End)
        {
            StateDic[ExState].OnStateExit();
        }
        ExState = AllEnum.States.End;
    }
    public void SetState(AllEnum.States _enum)
    {
        // 몬스터의 현재 상태를 지정된 상태로 설정
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
