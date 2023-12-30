using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//���¸� �Ǻ��ؼ� �������·� �ٲ���.
public class MONStateMachine : MonoBehaviour 
{
    public Monster owner;
    public Transform TargetTr; //������ transform
    Dictionary<AllEnum.States, State> StateDic = new Dictionary<AllEnum.States, State>();
    AllEnum.States ExState = AllEnum.States.End; //�������� üũ����
    AllEnum.States NowState = AllEnum.States.Idle; //������� üũ����
    private void Start()
    {
        
    }
    private void Update()
    {
        if (ExState == owner.NowState
            && owner.NowState != AllEnum.States.End 
            //2��, nowState�� End���¸� ���� ���ϵ��� �Ѵ�.
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
