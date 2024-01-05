using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveSkill : Skill
{
    private float colCenter = 7f;
    private float colSize = 12f;
    private float duration = 1f;
    float skillSpeed = 3;
    Coroutine boxCor = null;

    public void Init()
    {

    }
    
    public override void DoSkill()
    {
        Debug.Log("����" + GetHashCode());        
        if (this == null)
        {
            Debug.LogError("����");
        }
        else
        {
            if (orgInfo.index == 1) // ��
            {

            }
            else if (orgInfo.index == 2) // ������
            {

            }
            else if (orgInfo.index == 3) //��
            {
                Debug.Log("��ȣ ���");

                if (boxCor == null)
                {
                    //Debug.Log("�ڽ� ���");
                    Debug.Log("��ų �ߵ��߸¤�????? " + gameObject.activeSelf);
                    boxCor = StartCoroutine(GrowInBoxCollider());
                }
                //Debug.Log("�ڷ�ƾ����");

            }
            else if (orgInfo.index == 4) // �߷�
            {

            }
        }

        //StartCoroutine(DieTimer());    
    }

    //���޶�� ��û
    IEnumerator DieTimer()
    {
        yield return new WaitForSeconds(orgInfo.duration);        
        //SkillManager.Instance.SetOffSkill();
    }

    public IEnumerator GrowInBoxCollider()
    {
        Debug.Log("�Լ�����");
        BoxCollider col = transform.GetComponentInChildren<BoxCollider>();
        Debug.Log(col);
        if (col != null)
        {
            float elapsedTime = 0;
            float timecal = 0;
            while (elapsedTime <= duration)
            {
                elapsedTime += Time.deltaTime * skillSpeed;
                timecal = elapsedTime / duration;
                col.center = new Vector3(0, 0, colCenter * timecal);
                col.size = new Vector3(1, 1, colSize * timecal);
                Debug.Log("Counter1: " + col.center + " | Counter2: " + col.size);
                yield return null;
            }
        }
        boxCor = null; // �˾Ƽ� ����
    }

    public override void DoReset()
    {
    }

    
}
