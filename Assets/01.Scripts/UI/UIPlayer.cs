using UnityEngine;
using UnityEngine.UI;


public class UIPlayer : MonoBehaviour
{
    public Slider hp;
    public Slider mp;
    public Image ultimate;
    public Image exp;
    public UIPosionSlot[] uIPosionSlots;

    public void SetUI()
    {
        SetHPUI();
        SetMPUI();
        SetEXPUI();
        SetUltimateUI();
        SetPosionSlotUI();
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

        exp.fillAmount = nowExp == 0 ? 0 : nowExp % GameManager.Instance.player.Stat.maxExperience; ;
    }
    public void SetUltimateUI()
    {

    }
    public void SetPosionSlotUI()
    {
        for (int i = 0; i < uIPosionSlots.Length; i++)
        {
            uIPosionSlots[i].Init(DataManager.Instance.gameData.invenDatas.PosionItemDatas[(AllEnum.ItemList)i]);
        }
    }

}
