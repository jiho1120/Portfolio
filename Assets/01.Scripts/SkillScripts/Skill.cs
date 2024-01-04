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
        //��ų�ν��ؾ����ϵ�();
        DoSkill();
    }

    public virtual void /* ��ų�ν��ؾ����ϵ�*/DoSkill()
    { 
    }

    public void ShowData()
    {
        Debug.Log(orgInfo.name);
        Debug.Log(orgInfo.index);

    }
}
