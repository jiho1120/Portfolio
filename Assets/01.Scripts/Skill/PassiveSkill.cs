using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveSkill : Skill
{
    Coroutine passiveCor = null;
    float _currentNum;
    public void CorReset()
    {
        passiveCor = null;
    }

    public override void DoSkill(bool isPlayer)
    {
        PassiveSkillEffect(isPlayer); // 하나 키기
    }

    public void PassiveSkillEffect(bool isPlayer)
    {
        if (isPlayer)
        {
            _currentNum = GameManager.Instance.player.PassiveCurrentNum;
            gameObject.SetActive(true);
            transform.SetParent(GameManager.Instance.player.transform);
        }
        else
        {
            _currentNum = GameManager.Instance.boss.PassiveCurrentNum;
            gameObject.SetActive(true);
            transform.SetParent(GameManager.Instance.boss.transform);
        }
        
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
                passiveCor = StartCoroutine(FireSkill(isPlayer));
            }
        }
        else if (_currentNum == (int)AllEnum.SkillName.Heal) //Heal
        {
            if (passiveCor == null)
            {
                passiveCor = StartCoroutine(HealSkill(isPlayer));
            }
        }
        else if (_currentNum == (int)AllEnum.SkillName.Love)
        {
            if (passiveCor == null)
            {
                passiveCor = StartCoroutine(LoveSkill(isPlayer));
            }
        }
        else if (_currentNum == (int)AllEnum.SkillName.Wind)
        {
            if (passiveCor == null)
            {
                passiveCor = StartCoroutine(WindSKill(isPlayer));
            }
        }
        else
        {
            Debug.Log("무언가 잘못됨");
        }
    }
    public IEnumerator FireSkill(bool isPlayer)
    {
        float time = 0;
        if (isPlayer)
        {
            GameManager.Instance.player.playerStat.AddAttack(skillStat.effect);
            while (time <= skillStat.duration)
            {
                GameManager.Instance.player.SetHp(GameManager.Instance.player.Hp - skillStat.effect);
                yield return new WaitForSeconds(1f);
                time += 1f;
            }
            GameManager.Instance.player.playerStat.AddAttack(-skillStat.effect);

        }
        else
        {
            GameManager.Instance.boss.bossStat.AddAttack(skillStat.effect);
            while (time <= skillStat.duration)
            {

                GameManager.Instance.boss.bossStat.SetHealth(GameManager.Instance.boss.bossStat.health - skillStat.effect);
                yield return new WaitForSeconds(1f);
                time += 1f;

            }
            GameManager.Instance.boss.bossStat.AddAttack(-skillStat.effect);
        }
        yield return new WaitForSeconds(skillStat.cool);
    }
    public IEnumerator HealSkill(bool isPlayer)
    {
        if (isPlayer)
        {
            while (true)
            {
                GameManager.Instance.player.playerStat.AddHp(skillStat.effect);
                yield return new WaitForSeconds(skillStat.cool);
            }
        }
        else
        {
            while (true)
            {
                GameManager.Instance.boss.bossStat.AddHp(skillStat.effect);
                yield return new WaitForSeconds(skillStat.cool);
            }
        }

    }
    public IEnumerator LoveSkill(bool isPlayer)
    {
        float Range = 10f;

        if (isPlayer)
        {
            while (true)
            {
                Collider[] colliders = Physics.OverlapSphere(GameManager.Instance.player.transform.position, Range, GameManager.Instance.player.PlayerLayer);
                for (int i = 0; i < colliders.Length; i++)
                {
                    if (colliders[i].CompareTag("Monster"))
                    {
                        colliders[i].GetComponent<Monster>().monsterStat.AddDefence(-skillStat.effect);
                    }
                    else if (colliders[i].CompareTag("Boss"))
                    {
                      GameManager.Instance.boss.bossStat.AddDefence(-skillStat.effect);
                    }
                }
                yield return new WaitForSeconds(skillStat.cool);
            }
        }
        else
        {
            while (true)
            {
                Collider[] colliders = Physics.OverlapSphere(GameManager.Instance.boss.transform.position, Range, GameManager.Instance.boss.bossLayer);
                for (int i = 0; i < colliders.Length; i++)
                {
                    colliders[i].GetComponent<Player>().playerStat.AddDefence(-skillStat.effect);
                }
                yield return new WaitForSeconds(skillStat.cool);
            }
        }

    }

    public IEnumerator WindSKill(bool isPlayer)
    {
        float Range = 2.8f;
        if (isPlayer)
        {
            while (true)
            {
                Collider[] colliders = Physics.OverlapSphere(GameManager.Instance.player.transform.position, Range, GameManager.Instance.player.PlayerLayer);
                for (int i = 0; i < colliders.Length; i++)
                {
                    if (colliders[i].CompareTag("Monster"))
                    {
                        colliders[i].GetComponent<Monster>().TakeDamage(GameManager.Instance.player.Cri, skillStat.effect);
                    }
                    else if (colliders[i].CompareTag("Boss"))
                    {
                        colliders[i].GetComponent<Boss>().TakeDamage(GameManager.Instance.player.Cri, skillStat.effect);

                    }
                }
                yield return new WaitForSeconds(skillStat.cool);
            }
        }
        else
        {
            while (true)
            {
                Collider[] colliders = Physics.OverlapSphere(GameManager.Instance.boss.transform.position, Range, GameManager.Instance.boss.bossLayer);
                for (int i = 0; i < colliders.Length; i++)
                {
                    colliders[i].GetComponent<Player>().TakeDamage(GameManager.Instance.boss.bossStat.criticalChance, skillStat.effect);
                }
                yield return new WaitForSeconds(skillStat.cool);
            }
        }

    }

    public override void DoReset()
    {
        skillStat.SetInUse(false);
        gameObject.SetActive(false);
    }
}
