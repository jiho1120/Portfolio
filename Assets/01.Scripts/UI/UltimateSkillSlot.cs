using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UltimateSkillSlot : MonoBehaviour
{
    public Image GuageImage;
    public Image coolImg;

    public void SetUltimateUI()
    {
        float nowUlti = GameManager.Instance.player.Stat.ultimateGauge;
        GuageImage.fillAmount = nowUlti / GameManager.Instance.player.Stat.maxUltimateGauge;

        if (GuageImage.fillAmount == 1)
        {
            coolImg.gameObject.SetActive(false);
        }
        else
        {
            coolImg.gameObject.SetActive(true);
        }
    }
}