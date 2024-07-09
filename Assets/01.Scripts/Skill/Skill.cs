using System.Collections;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using static AllEnum;

public class Skill : MonoBehaviour,Initialize
{
    protected int enemyLayer;


    public SkillData skilldata;
    [SerializeField] protected NewSkillType skillType;// 스킬 타입
    public SkillName skillName { get; private set; } // 스킬 이름

    public virtual void SetEnemyLayer(int enemyLayer)
    {
        this.enemyLayer = enemyLayer;
    }

    // 스킬 레벨업시 이걸로 
    public void SetSkillData(SkillData _skillData)
    {
        skilldata = _skillData;
    }
    public SkillData GetSkillData()
    {
        return skilldata;
    }

    // 스킬 지속시간만큼 존재하다가 꺼짐
    public virtual void Activate()
    {
        gameObject.SetActive(true);
        ImplementEffects();

    }

    public virtual void Deactivate()
    {
        gameObject.SetActive(false);
    }
    

    /// <summary>
    /// active할때 한번 실행시키는 함수 추가적인 구현
    /// </summary>
    protected virtual void ImplementEffects()
    {
    } 
    
    public virtual void SetSKillPos()
    {
        Vector3 pos = GameManager.Instance.player.transform.position;
        Quaternion rot = GameManager.Instance.player.transform.rotation;
        if (skillName == SkillName.Gravity)
        {
            Vector3 spawnOffset = transform.rotation * new Vector3(0, 0.5f, 1) * 10f;
            rot = Quaternion.Euler(transform.rotation.eulerAngles);
            pos += spawnOffset;
        }
    }
    //물리적 스킬 구현 x ex) 디버프
    protected virtual void OnTriggerEnter(Collider other)
    {

    }


    //물리적 스킬 구현 ex) 넉백
    protected virtual void OnCollisionEnter(Collision collision)
    {

    }
}
