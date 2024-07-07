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
        int num = -1; // 초기값 설정
        switch (skillName)
        {
            case SkillName.AirSlash:
                num = 0;
                break;
            case SkillName.AirCircle:
                num = 1;
                break;
            case SkillName.Ground:
                num = 2;
                break;
            case SkillName.Gravity:
                num = 3;
                break;
            case SkillName.End:
                num = -1;
                break;
            default:
                Debug.LogError("Invalid skill name");
                break;
        }

        if (num >= 0 && num < ResourceManager.Instance.SkillSprite.Length)
        {
            skillImg.sprite = ResourceManager.Instance.SkillSprite[num];
        }
        else
        {
            Debug.LogError("Invalid sprite index or skill not assigned properly");
        }
    }
    void SetSKillCool()
    {
        skillCoolTime.text = DataManager.Instance.gameData.playerData.skillDict[skillName].cool.ToString();
        
    }
    
    public void SetUseSKillTime()
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
