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
    
    private void OnCollisionEnter(Collision collision) // 1��,3��
    {
        //��ų�ν��ؾ����ϵ�();
        DoSkill();
    }
    private void OnCollisionStay(Collision collision) // 4�� ��ų , 2���� �ڷ�ƾ���� �ð����༭ ���ֱ�
    {
        
    }

    public virtual void /* ��ų�ν��ؾ����ϵ�*/DoSkill()
    { 
    }

    
    
    
}
