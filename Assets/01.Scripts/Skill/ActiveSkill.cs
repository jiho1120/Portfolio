using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveSkill : MonoBehaviour
{
    public GameObject[] activeSkillList;
    public SkillInfo skillInfo;
    public SOSkill[] soSkills;

    private float colCenter = 7f;
    private float colSize = 12f;
    private float duration = 1f;
    float skillSpeed = 3;
    Coroutine boxCor = null; 

    public void SetSkillInfo(GameObject instantiatedSkill, int number)
    {
        if (instantiatedSkill == null)
        {
            print("없어");
        }
        else
        {
            if (number == 0 && boxCor == null)
            {
                boxCor = StartCoroutine(GrowInBoxCollider(instantiatedSkill));
                skillInfo.SetSkillData(soSkills[0]);
            }
            else if (number == 1)
            {

            }
            else if (number == 2)
            {

            }
            else if (number == 3)
            {

            }
        }
    }


    public void CreateSkill(bool setParent, int number, Vector3 pos) // 이변수들을 나중에 스킬이 가지고 있어야함
    {
        GameObject instantiatedSkill = Instantiate(activeSkillList[number], pos, GameManager.Instance.player.transform.GetChild(0).rotation);

        // 생성된 스킬을 플레이어의 자식으로 설정
        if (setParent)
        {
            instantiatedSkill.transform.SetParent(GameManager.Instance.player.transform.GetChild(0), true);
        }
        SetSkillInfo(instantiatedSkill, number);
        instantiatedSkill.SetActive(true);
    }

    public IEnumerator GrowInBoxCollider(GameObject obj)
    {
        BoxCollider col = obj.transform.GetChild(0).GetComponentInChildren<BoxCollider>();
        if (col != null)
        {
            float elapsedTime = 0;
            float timecal = 0;
            while (elapsedTime <= duration)
            {
                elapsedTime += Time.deltaTime * skillSpeed;
                timecal = elapsedTime / duration;
                col.center = new Vector3(0, 0, colCenter* timecal);
                col.size = new Vector3(1, 1, colSize * timecal);
                Debug.Log("Counter1: " + col.center + " | Counter2: " + col.size);
                yield return null;
            }
        }
        boxCor = null; // 알아서 끝남
    }
    
    public IEnumerator TimesAttack(Monster Enemy)
    {
        skillInfo.SetSkillData(soSkills[1]);
        skillInfo.duration = 3f;
        float duration = 0;
        while (skillInfo.duration < duration)
        {
            duration += Time.deltaTime;
            Enemy.TakeDamage(10, 10);
            yield return new WaitForSeconds(0.1f);
        }
    }

}
