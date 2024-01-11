using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerConditionUI : MonoBehaviour
{
    public Slider hp;
    public Slider mp;
    public Slider exp;
    public GameObject item;
    public SkillSlot[] skill; // 궁이 0임 일반 스킬은 1부터

    public void Init()
    {
        for (int i = 0; i < skill.Length; i++)
        {
            skill[i].SetSkill();
            skill[i].SetIcon();
            skill[i].SetCoolTime();
            skill[i].gauge.fillAmount = 0;
        }
    }

    public void SetUI()
    {
        hp.maxValue = GameManager.Instance.player.playerStat.maxHealth;
        hp.value = GameManager.Instance.player.playerStat.health;
        mp.maxValue = GameManager.Instance.player.playerStat.maxMana;
        mp.value = GameManager.Instance.player.playerStat.mana;
        exp.maxValue = GameManager.Instance.player.playerStat.maxExperience;
        exp.value = GameManager.Instance.player.playerStat.experience;

        SuperCoolImg();
    }

    public void SuperCoolImg()
    {
        skill[3].gauge.fillAmount = GameManager.Instance.player.playerStat.ultimateGauge / GameManager.Instance.player.playerStat.maxUltimateGauge;

        Color color = skill[3].gauge.color;
        if (skill[3].gauge.fillAmount == 1)
        {
            color.a = 1;
        }
        else
        {
            color.a = 0.15f;
        }
        skill[3].gauge.color = color;
    }
}