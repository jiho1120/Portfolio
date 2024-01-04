using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    public int Index;
    public SOSkill orgInfo;
  
    public void SetInfo(SOSkill _Info)
    {
        orgInfo = _Info;
    }
    
    private void OnCollisionEnter(Collision collision) // 1번,3번
    {
        //스킬로써해야할일들();
        DoSkill();
    }
    private void OnCollisionStay(Collision collision) // 4번 스킬 , 2번도 코루틴으로 시간텀줘서 딜넣기
    {
        
    }

    public virtual void /* 스킬로써해야할일들*/DoSkill()
    { 
    }

    
    
    
}
