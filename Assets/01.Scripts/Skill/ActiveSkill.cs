using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveSkill : MonoBehaviour
{
    public GameObject[] activeSkillList;



    // ���� ������Ʈ�� �ҷ���  �׸��� ���Ͻ���
    public GameObject GetSkillObject(int number) 
    {
        return activeSkillList[number] != null ? activeSkillList[number] : null;
    }

    // ���Ͻ�Ų�� �޾Ƽ� ������ ������
    public GameObject SetSkillInfo(bool setParent, int number)
    {
        GameObject skill = GetSkillObject(number);
        if (skill == null)
        {
            print("����");
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
                print("ũ�� �ٲ�");
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
    // ���� �����Ѱ� ��ȯ
    public void CreateSkill(bool setParent, int number, Vector3 pos) // �̺������� ���߿� ��ų�� ������ �־����
    {
        GameObject skill = SetSkillInfo(setParent,number);
        GameObject instantiatedSkill = Instantiate(skill, pos, GameManager.Instance.player.transform.GetChild(0).rotation); 

        // ������ ��ų�� �÷��̾��� �ڽ����� ����
        if (setParent)
        {
            instantiatedSkill.transform.SetParent(GameManager.Instance.player.transform.GetChild(0), true);
        }
        
        instantiatedSkill.SetActive(true);
        
    }
    

}
