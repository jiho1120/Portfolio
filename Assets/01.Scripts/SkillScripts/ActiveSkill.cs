//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class ActiveSkill : Skill//MonoBehaviour
//{

//public override void DoSkill()
//{ 
//    //��Ƽ���� ���� �ؾ�����
//}

//    public GameObject[] activeSkillList; // ���߿� ����Ʈ�� �ٲ㼭 ��ġ�� ã�� ������� �ٲ���ְ�
//    public SkillInfo skillInfo;
//    public SOSkill[] soSkills;

//    private float colCenter = 7f;
//    private float colSize = 12f;
//    private float duration = 1f;
//    float skillSpeed = 3;
//    Coroutine boxCor = null; 

//    public void Init()
//    {

//    }

//    public void SetSkillInfo(GameObject instantiatedSkill, int number)
//    {
//        if (instantiatedSkill == null)
//        {
//            print("����");
//        }
//        else
//        {
//            if (number == 0 && boxCor == null)
//            {
//                boxCor = StartCoroutine(GrowInBoxCollider(instantiatedSkill));
//                skillInfo.SetSkillData(soSkills[0]);
//            }
//            else if (number == 1)
//            {

//            }
//            else if (number == 2)
//            {

//            }
//            else if (number == 3)
//            {

//            }
//        }
//    }


//    public void CreateSkill(bool setParent, int number, Vector3 pos) // �̺������� ���߿� ��ų�� ������ �־����
//    {
//        GameObject instantiatedSkill = Instantiate(activeSkillList[number], pos, GameManager.Instance.player.transform.GetChild(0).rotation);

//        // ������ ��ų�� �÷��̾��� �ڽ����� ����
//        if (setParent)
//        {
//            instantiatedSkill.transform.SetParent(GameManager.Instance.player.transform.GetChild(0), true);
//        }
//        SetSkillInfo(instantiatedSkill, number);
//        instantiatedSkill.SetActive(true);
//    }

//    public IEnumerator GrowInBoxCollider(GameObject obj)
//    {
//        BoxCollider col = obj.transform.GetChild(0).GetComponentInChildren<BoxCollider>();
//        if (col != null)
//        {
//            float elapsedTime = 0;
//            float timecal = 0;
//            while (elapsedTime <= duration)
//            {
//                elapsedTime += Time.deltaTime * skillSpeed;
//                timecal = elapsedTime / duration;
//                col.center = new Vector3(0, 0, colCenter* timecal);
//                col.size = new Vector3(1, 1, colSize * timecal);
//                Debug.Log("Counter1: " + col.center + " | Counter2: " + col.size);
//                yield return null;
//            }
//        }
//        boxCor = null; // �˾Ƽ� ����
//    }
    

//}
