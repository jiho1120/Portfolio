using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveSkill : MonoBehaviour
{
    public GameObject[] activeSkillList;



    // 게임 오브젝트만 불러와  그리고 리턴시켜
    public GameObject GetSkillObject(int number) 
    {
        return activeSkillList[number] != null ? activeSkillList[number] : null;
    }

    // 리턴시킨거 받아서 정보를 세팅해
    public GameObject SetSkillInfo(bool setParent, int number)
    {
        GameObject skill = GetSkillObject(number);
        if (skill == null)
        {
            print("없어");
        }
        else
        {
            if (number == 0)
            {
                BoxCollider col = skill.transform.GetChild(0).GetComponentInChildren<BoxCollider>();
                if (col != null)
                {
                    col.center = new Vector3(0, 0, 7);
                    col.size = new Vector3(1, 1, 14);
                }
                print("크기 바뀜");
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
        return skill;
    }
    // 정보 세팅한거 소환
    public void CreateSkill(bool setParent, int number, Vector3 pos) // 이변수들을 나중에 스킬이 가지고 있어야함
    {
        GameObject skill = SetSkillInfo(setParent,number);
        GameObject instantiatedSkill = Instantiate(skill, pos, GameManager.Instance.player.transform.GetChild(0).rotation); 

        // 생성된 스킬을 플레이어의 자식으로 설정
        if (setParent)
        {
            instantiatedSkill.transform.SetParent(GameManager.Instance.player.transform.GetChild(0), true);
        }
        
        instantiatedSkill.SetActive(true);
        
    }
    

}
