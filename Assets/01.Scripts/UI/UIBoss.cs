using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIBoss : MonoBehaviour
{
    public Slider hp;
    public Slider mp;

    public void SetBossUI()
    {
        SetHPUI();
        SetMPUI();
    }
    public void SetHPUI()
    {
        hp.maxValue = GameManager.Instance.boss.Stat.maxHp;
        hp.value = GameManager.Instance.boss.Stat.hp;
    }
    public void SetMPUI()
    {
        mp.maxValue = GameManager.Instance.boss.Stat.maxMp;
        mp.value = GameManager.Instance.boss.Stat.mp;
    }
    
}
