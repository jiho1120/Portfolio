using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Monster : MonoBehaviour
{
    public AllEnum.MonsterType monType;
    public AllEnum.States NowState = AllEnum.States.End;//현재상태    
    MonsterAnimation anim; //얘는 진짜 단순히 애니메이션 출력...    
    NavMeshAgent agent;
    public NavMeshAgent Agent => agent;
    public Vector3 dir;
    public GameObject explosionEffect;
    Rigidbody rb;
    public Transform attackPos;
    


    
}
