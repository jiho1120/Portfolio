using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerConditionUI : MonoBehaviour
{
    public Slider hp;
    public Slider mp;
    public Slider exp;
    public Image super;
    //public Image skill;
    //public Image item;


    public void SetUI()
    {
        hp.maxValue = GameManager.Instance.player.playerStat.maxHealth;
        hp.value = GameManager.Instance.player.playerStat.health;
        mp.maxValue = GameManager.Instance.player.playerStat.maxMana;
        mp.value = GameManager.Instance.player.playerStat.mana;
        exp.maxValue = GameManager.Instance.player.playerStat.maxExperience;
        exp.value = GameManager.Instance.player.playerStat.experience;
        super.fillAmount = GameManager.Instance.player.playerStat.ultimateGauge / GameManager.Instance.player.playerStat.maxUltimateGauge;
    }
    
}