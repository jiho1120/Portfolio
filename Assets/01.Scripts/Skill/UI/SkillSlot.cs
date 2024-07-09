using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using static AllEnum;


public class SkillSlot : MonoBehaviour
{
    public SkillName skillName;
    [SerializeField] Image skillImg;
    [SerializeField] Image gaugeImg;
    [SerializeField] Text skillCoolTime;

    float useTime = 0;

    public void Init()
    {
        skillImg = GetComponentInChildren<Image>();
        gaugeImg = skillImg.transform.GetChild(0).GetComponent<Image>();
        skillCoolTime = GetComponentInChildren<Text>();
        SetSKillImg();
        gaugeImg.fillAmount = 0;
        SetSKillCool();
    }

    void SetSKillImg()
    {
        skillImg.sprite = ResourceManager.Instance.GetSprite(DictName.SkillSpriteDict, skillName.ToString());
    }
    void SetSKillCool()
    {
        skillCoolTime.text = DataManager.Instance.gameData.playerData.skillDict[skillName].cool.ToString();

    }

    public void SetUseSkillTime()
    {
        useTime = 0;
        gaugeImg.fillAmount = 1;
        int.TryParse(skillCoolTime.text, out int val);
        StartCoroutine(SkillCoolCor(val));
    }
    private IEnumerator SkillCoolCor(float val)
    {
        while (useTime <= val)
        {
            useTime += Time.deltaTime;
            yield return null;
            gaugeImg.fillAmount = 1 - useTime / val;
        }
    }

}
