using UnityEngine;
using UnityEngine.UI;


public class UIPlayer : MonoBehaviour
{
    public Slider hp;
    public Slider mp;
    public Image ultimate;
    public Image exp;
    public UIPosionSlot[] uiPosionSlots;
    public SkillSlot[] uiSkillSlots;


    public void SetPlayerUI()
    {
        SetHPUI();
        SetMPUI();
        SetEXPUI();
        SetUltimateUI();
        SetPosionSlotUI();
        SetSkillSlotUI();
    }
    public void SetHPUI()
    {
        hp.maxValue = GameManager.Instance.player.Stat.maxHp;
        hp.value = GameManager.Instance.player.Stat.hp;
    }
    public void SetMPUI()
    {
        mp.maxValue = GameManager.Instance.player.Stat.maxMp;
        mp.value = GameManager.Instance.player.Stat.mp;
    }
    public void SetEXPUI()
    {
        float nowExp = GameManager.Instance.player.Stat.experience;

        exp.fillAmount =  nowExp /GameManager.Instance.player.Stat.maxExperience;
    }
    public void SetUltimateUI()
    {
        float nowUlti = GameManager.Instance.player.Stat.ultimateGauge;
        ultimate.fillAmount = nowUlti / GameManager.Instance.player.Stat.maxUltimateGauge;

    }
    public void SetPosionSlotUI()
    {
        for (int i = 0; i < uiPosionSlots.Length; i++)
        {
            uiPosionSlots[i].Init(DataManager.Instance.gameData.invenDatas.PosionItemDatas[(AllEnum.ItemList)i]);
        }
    }

    public void SetSkillSlotUI()
    {
        for (int i = 0; i < uiSkillSlots.Length; i++)
        {
            uiSkillSlots[i].Init();
        }
    }

}
