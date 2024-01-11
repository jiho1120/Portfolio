using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerConditionUI : MonoBehaviour
{
    public Slider hp;
    public Slider mp;
    public Slider exp;
    public GameObject super;
    public GameObject item; // 굳이? 어차피 수동으로 세팅함 나중에 데이터만 바꾸면 됨 그때 접근하기위해 필요
    public GameObject skill; // 똑같음

    public void Init()
    {
        for (int i = 0; i < skill.transform.childCount; i++)
        {
            SkillSlot sk = skill.transform.GetChild(i).GetComponent<SkillSlot>();
            sk.SetSkill();
            sk.SetIcon();
        }
        SkillSlot superSlot = super.GetComponent<SkillSlot>();
        superSlot.SetSkill();
        superSlot.SetIcon();
    }

    public void SetUI()
    {
        hp.maxValue = GameManager.Instance.player.playerStat.maxHealth;
        hp.value = GameManager.Instance.player.playerStat.health;
        mp.maxValue = GameManager.Instance.player.playerStat.maxMana;
        mp.value = GameManager.Instance.player.playerStat.mana;
        exp.maxValue = GameManager.Instance.player.playerStat.maxExperience;
        exp.value = GameManager.Instance.player.playerStat.experience;
        Image gauge = super.transform.GetChild(0).GetComponent<Image>();
            gauge.fillAmount = GameManager.Instance.player.playerStat.ultimateGauge / GameManager.Instance.player.playerStat.maxUltimateGauge;
        Color color = gauge.color;
        if (gauge.fillAmount == 1)
        {
            color.a = 1;
        }
        else
        {
            color.a = 0.15f;
        }
        gauge.color = color;

        //for (int i = 0; i < skill.transform.childCount; i++) // 쿨타임 보여주기
        //{
        //    SkillSlot sk = skill.transform.GetChild(i).GetComponent<SkillSlot>();

        //}

    }
    

}