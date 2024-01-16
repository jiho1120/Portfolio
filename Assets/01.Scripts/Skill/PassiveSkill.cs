using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveSkill : Skill
{
    Coroutine passiveCor = null;

    //Coroutine firePassiveCor = null;
    //Coroutine healPassiveCor = null;
    //Coroutine lovePassiveCor = null;
    //Coroutine windPassiveCor = null;


    public override void DoSkill()
    {
        DoPassiveSkill(SkillManager.Instance.PassiveCurrentNum); // �ϳ� Ű��
    }

    public void DoPassiveSkill(int _currentNum)
    {
        gameObject.SetActive(true);
        transform.SetParent(GameManager.Instance.player.transform);
        transform.localPosition = Vector3.up;
        skillStat.SetInUse(true);

        if (passiveCor != null)
        {
            StopCoroutine(passiveCor);
            passiveCor = null;
        }
        Debug.Log("�ڷ�ƾ ������");
        if (_currentNum == (int)AllEnum.SkillName.Fire) //Fire
        {
            if (passiveCor == null)
            {
                passiveCor = StartCoroutine(FireSkill());
            }
        }
        else if (_currentNum == (int)AllEnum.SkillName.Heal) //Heal
        {
            if (passiveCor == null)
            {
                passiveCor = StartCoroutine(HealSkill());
            }
        }
        else if (_currentNum == (int)AllEnum.SkillName.Love)
        {
            if (passiveCor == null)
            {
                passiveCor = StartCoroutine(LoveSkill());
            }
        }
        else if (_currentNum == (int)AllEnum.SkillName.Wind)
        {
            if (passiveCor == null)
            {
                passiveCor = StartCoroutine(WindSKill());
            }
        }
        else
        {
            Debug.Log("���� �߸���");
        }
    }
    public IEnumerator FireSkill()
    {
        Debug.Log("�ڷ�ƾ ���� ��");
        GameManager.Instance.player.playerStat.AddAttack(skillStat.effect);
        while (true)
        {
            GameManager.Instance.player.playerStat.AddHp(-skillStat.effect);
            yield return new WaitForSeconds(skillStat.cool);
        }
    }
    public IEnumerator HealSkill()
    {
        Debug.Log("�ڷ�ƾ ���� ��");
        while (true)
        {
            GameManager.Instance.player.playerStat.AddHp(skillStat.effect);
            yield return new WaitForSeconds(skillStat.cool);
        }
    }
    public IEnumerator LoveSkill()
    {
        Debug.Log("�ڷ�ƾ ���� ��Ȥ");
        float Range = 10f;

        while (true)
        {
            Collider[] colliders = Physics.OverlapSphere(GameManager.Instance.player.transform.position, Range, monsterLayer);
            for (int i = 0; i < colliders.Length; i++)
            {
                Debug.Log("�ڷ�ƾ ���� ���� : " + colliders[i].gameObject.name);
                colliders[i].GetComponent<Monster>().ReduceDefence(skillStat.effect);
                Debug.Log(colliders[i].GetComponent<Monster>().monsterStat.defense);
            }
            yield return new WaitForSeconds(skillStat.cool);
        }
    }

    public IEnumerator WindSKill()
    {
        Debug.Log("�ڷ�ƾ ���� + �ٶ�");

        float Range = 2.8f;
        while (true)
        {
            Collider[] colliders = Physics.OverlapSphere(GameManager.Instance.player.transform.position, Range, monsterLayer);
            for (int i = 0; i < colliders.Length; i++)
            {
                Debug.Log("�ڷ�ƾ ���� ���� : " + colliders[i].gameObject.name);
                colliders[i].GetComponent<Monster>().TakeDamage(GameManager.Instance.player.Cri, skillStat.effect);
            }
            yield return new WaitForSeconds(skillStat.cool);
            Debug.Log("��ų ��" + skillStat.cool);

        }
    }

    public override void DoReset()
    {
        skillStat.SetInUse(false);
        gameObject.SetActive(false);

        Debug.Log("����");
    }
}
