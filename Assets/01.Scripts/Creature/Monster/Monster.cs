using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Monster : MonoBehaviour
{
    public AllEnum.MonsterType monType;
    public AllEnum.States NowState = AllEnum.States.End;//�������    
    MonsterAnimation anim; //��� ��¥ �ܼ��� �ִϸ��̼� ���...    
    NavMeshAgent agent;
    public NavMeshAgent Agent => agent;
    public Vector3 dir;
    public GameObject explosionEffect;
    Rigidbody rb;
    public Transform attackPos;
    


    
}
