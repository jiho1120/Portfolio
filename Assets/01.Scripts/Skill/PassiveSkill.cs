using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveSkill : Skill
{
    int currentNum=0;
    Coroutine firePassiveCor = null;
    Coroutine healPassiveCor = null;
    Coroutine lovePassiveCor = null;
    Coroutine windPassiveCor = null;

   
    public override void DoSkill()
    {
        currentNum = SkillManager.Instance.PassiveCurrentNum;
        DoReset(); // �� ����
        Debug.Log("���� ���");
        DoPassiveSkill(currentNum); // �ϳ� Ű��
        Debug.Log("�нú� ���");
        SkillManager.Instance.PassiveCurrentNum++;
        if (SkillManager.Instance.PassiveCurrentNum >= (int)AllEnum.SkillName.End) // �ε��� �ѱ�� ó������ ����
        {
            SkillManager.Instance.PassiveCurrentNum = (int)AllEnum.SkillName.Fire;
        }
        Debug.Log("��ų ��ü ���");
    }

    public void DoPassiveSkill(int _currentNum)
    {
        Debug.Log(_currentNum);
        Skill skill = SkillManager.Instance.skillDict[(AllEnum.SkillName)_currentNum];
        SkillManager.Instance.SetSkillPos(skill, GameManager.Instance.player.transform.position);
        Debug.Log(skill);
        skill.gameObject.SetActive(true);
        skill.skillStat.SetInUse(true);
        Debug.Log("�ڷ�ƾ ������");

        if (_currentNum == (int)AllEnum.SkillName.Fire) //Fire
        {
            if (firePassiveCor == null)
            {
                firePassiveCor = StartCoroutine(FireSkill(skill));
            }
        }
        else if (_currentNum == (int)AllEnum.SkillName.Heal) //Heal
        {
            if (healPassiveCor == null)
            {
                healPassiveCor = StartCoroutine(HealSkill(skill));
            }
        }
        else if (_currentNum == (int)AllEnum.SkillName.Love)
        {
            if (lovePassiveCor == null)
            {
                lovePassiveCor = StartCoroutine(LoveSkill(skill));
            }
        }
        else if (_currentNum == (int)AllEnum.SkillName.Wind)
        {
            if (windPassiveCor == null)
            {
                windPassiveCor = StartCoroutine(WindSKill(skill));
            }
        }
        else
        {
            Debug.Log("���� �߸���");
        }
        Debug.Log("�ڷ�ƾ �����ϰ� ����");
    }
    public IEnumerator FireSkill(Skill skill)
    {
        Debug.Log("�ڷ�ƾ ����");
        GameManager.Instance.player.playerStat.AddAttack(skill.skillStat.effect);
        while (true)
        {
            GameManager.Instance.player.playerStat.AddHp(-skill.skillStat.effect);
            yield return new WaitForSeconds(skill.skillStat.cool);
            Debug.Log("��ų ��" + skill.skillStat.cool);
        }
    }
    public IEnumerator HealSkill(Skill skill)
    {
        Debug.Log("�ڷ�ƾ ����");

        while (true)
        {
            GameManager.Instance.player.playerStat.AddHp(skill.skillStat.effect);
            yield return new WaitForSeconds(skill.skillStat.cool);
            Debug.Log("��ų ��" + skill.skillStat.cool);

        }
    }
    public IEnumerator LoveSkill(Skill skill)
    {
        Debug.Log("�ڷ�ƾ ����");

        Vector3 pos = GameManager.Instance.player.transform.position;
        float Range = 10f;
        while (true)
        {
            Collider[] colliders = Physics.OverlapSphere(pos, Range);
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].CompareTag("Monster"))
                {
                    colliders[i].GetComponent<Monster>().ReduceDefence(skill.skillStat.effect);
                }
            }
            yield return new WaitForSeconds(skill.skillStat.cool);
            Debug.Log("��ų ��" + skill.skillStat.cool);

        }
    }

    public IEnumerator WindSKill(Skill skill)
    {
        Debug.Log("�ڷ�ƾ ����");

        Vector3 pos = GameManager.Instance.player.transform.position;
        float Range = 2.8f;
        while (true)
        {
            Collider[] colliders = Physics.OverlapSphere(pos, Range);
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].CompareTag("Monster"))
                {
                    colliders[i].GetComponent<Monster>().ReduceDefence(skill.skillStat.effect);
                }
            }
            yield return new WaitForSeconds(skill.skillStat.cool);
            Debug.Log("��ų ��" + skill.skillStat.cool);

        }
    }
    public Skill GetSkill(int _currentNum)
    {
        return SkillManager.Instance.skillDict[(AllEnum.SkillName)_currentNum];
    }

    public override void DoReset()
    {
        Skill skill = GetSkill(currentNum);
        skill.gameObject.SetActive(false);
        skill.skillStat.SetInUse(false);
        if (skill.skillStat.skillName == AllEnum.SkillName.Fire)
        {
            if (firePassiveCor != null)
            {
                StopCoroutine(firePassiveCor);
                firePassiveCor = null;
            }
        }
        else if (skill.skillStat.skillName == AllEnum.SkillName.Heal)
        {
            if (healPassiveCor != null)
            {
                StopCoroutine(healPassiveCor);
                healPassiveCor = null;
            }
        }
        else if (skill.skillStat.skillName == AllEnum.SkillName.Love)
        {
            if (lovePassiveCor != null)
            {
                StopCoroutine(lovePassiveCor);
                lovePassiveCor = null;
            }
        }
        else if (skill.skillStat.skillName == AllEnum.SkillName.Wind)
        {
            if (windPassiveCor != null)
            {
                StopCoroutine(windPassiveCor);
                windPassiveCor = null;
            }
        }
        Debug.Log("����");
    }
}
