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
        hp.maxValue = GameManager.Instance.player.MaxHp;
        hp.value = GameManager.Instance.player.Hp;
    }
    public void SetMPUI()
    {
        mp.maxValue = GameManager.Instance.player.MaxMp;
        mp.value = GameManager.Instance.player.Mp;
    }
    public void SetEXPUI()
    {
        float nowExp = DataManager.Instance.gameData.playerData.playerStat.experience;

        exp.fillAmount = nowExp == 0 ? 0 : nowExp % DataManager.Instance.gameData.playerData.playerStat.maxExperience; ;
    }
    public void SetUltimateUI()
    {

    }
    public void SetPosionSlotUI()
    {
        for (int i = 0; i < uIPosionSlots.Length; i++)
        {
            //DataManager.Instance.LoadInvenInfo();
            uIPosionSlots[i].Init(DataManager.Instance.gameData.invenDatas.PosionItemDatas[(AllEnum.ItemList)i]);
        }
    }

}
