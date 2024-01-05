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
        Debug.Log("실행" + GetHashCode());        
        if (this == null)
        {
            Debug.LogError("없어");
        }
        else
        {
            if (orgInfo.index == 1) // 원
            {

            }
            else if (orgInfo.index == 2) // 슬래쉬
            {

            }
            else if (orgInfo.index == 3) //땅
            {
                Debug.Log("번호 통과");

                if (boxCor == null)
                {
                    //Debug.Log("박스 통과");
                    Debug.Log("스킬 발동중맞ㅇ????? " + gameObject.activeSelf);
                    boxCor = StartCoroutine(GrowInBoxCollider());
                }
                //Debug.Log("코루틴문제");

            }
            else if (orgInfo.index == 4) // 중력
            {

            }
        }

        //StartCoroutine(DieTimer());    
    }

    //꺼달라는 요청
    IEnumerator DieTimer()
    {
        yield return new WaitForSeconds(orgInfo.duration);        
        //SkillManager.Instance.SetOffSkill();
    }

    public IEnumerator GrowInBoxCollider()
    {
        Debug.Log("함수들어옴");
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
        boxCor = null; // 알아서 끝남
    }

    public override void DoReset()
    {
    }

    
}
