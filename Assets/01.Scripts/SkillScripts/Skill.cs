using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    public int Index;
    SOSkill orgInfo;
    public void SetInfo(SOSkill _Info)
    {
        orgInfo = _Info;
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        //스킬로써해야할일들();
        DoSkill();
    }

    public virtual void /* 스킬로써해야할일들*/DoSkill()
    { 
    }

    public void ShowData()
    {
        Debug.Log(orgInfo.name);
        Debug.Log(orgInfo.index);

    }
}
