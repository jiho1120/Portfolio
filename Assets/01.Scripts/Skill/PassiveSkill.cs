using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveSkill : Skill
{
    Coroutine passiveCor = null;

    public override void DoSkill()
    {
        DoPassiveSkill(SkillManager.Instance.PassiveCurrentNum); // 하나 키기
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
            Debug.Log("무언가 잘못됨");
        }
    }
    public IEnumerator FireSkill()
    {
        GameManager.Instance.player.playerStat.AddAttack(skillStat.effect);
        while (true)
        {
            GameManager.Instance.player.SetHp(GameManager.Instance.player.Hp + skillStat.effect); // 값을 -로 지정해서 뺄려면 더해야함
            yield return new WaitForSeconds(skillStat.cool);
        }
    }
    public IEnumerator HealSkill()
    {
        while (true)
        {
            GameManager.Instance.player.playerStat.AddHp(skillStat.effect);
            yield return new WaitForSeconds(skillStat.cool);
        }
    }
    public IEnumerator LoveSkill()
    {
        float Range = 10f;

        while (true)
        {
            Collider[] colliders = Physics.OverlapSphere(GameManager.Instance.player.transform.position, Range, monsterLayer);
            for (int i = 0; i < colliders.Length; i++)
            {
                colliders[i].GetComponent<Monster>().ReduceDefence(skillStat.effect);
            }
            yield return new WaitForSeconds(skillStat.cool);
        }
    }

    public IEnumerator WindSKill()
    {
        float Range = 2.8f;
        while (true)
        {
            Collider[] colliders = Physics.OverlapSphere(GameManager.Instance.player.transform.position, Range, monsterLayer);
            for (int i = 0; i < colliders.Length; i++)
            {
                colliders[i].GetComponent<Monster>().TakeDamage(GameManager.Instance.player.Cri, skillStat.effect);
            }
            yield return new WaitForSeconds(skillStat.cool);

        }
    }

    public override void DoReset()
    {
        skillStat.SetInUse(false);
        gameObject.SetActive(false);
    }
}
