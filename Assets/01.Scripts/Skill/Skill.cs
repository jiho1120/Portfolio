using UnityEngine;
using static AllEnum;

public abstract class Skill : MonoBehaviour,Initialize
{
    protected SkillData skilldata;
    [SerializeField] NewSkillType skillType;// 스킬 타입
    [SerializeField] SkillName skillName; // 스킬 이름
    public ObjectType caster { get; private set; }
    [SerializeField] bool setParent; // 스킬이 플레이어를 따라다닐지 // 그래비티랑 그라운드만 flase
    [SerializeField] bool inUse; //false일시 스킬나감


    // 스킬 레벨업시 이걸로 
    public void Init()
    {

    }
    
    public void Activate()
    {
        if (caster == ObjectType.Player)
        {
            skilldata =  DataManager.Instance.gameData.playerData.skillDict[skillName];

        }
        else if (caster == ObjectType.Boss)
        {
            skilldata = DataManager.Instance.gameData.bossData.skillDict[skillName];
        }
        gameObject.SetActive(true);

    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
    }
    

    protected virtual void OnTriggerEnter(Collider other)
    {

    }
    protected virtual void OnCollisionEnter(Collision collision)
    {

    }

    
    //    // 스킬의 시전자 판별 // 사용할때 정보를 받아오자

    //    // 스킬의 상태 판별(딜인지 힐인지)
    //    // 시전자가 플레이어면 에니미에 레이어랑 비교해서 딜


    //    string hitLayerName;
    //    int hitObjLayerIndex;
    //    int idx = collision.gameObject.layer;

    //    switch (caster)
    //    {
    //        case ObjectType.Boss:
    //            hitLayerName = "Player";
    //            break;
    //        case ObjectType.Player:
    //            hitLayerName = "Enemy";
    //            break;
    //        default:
    //            break;
    //    }

    //    //hitObjLayerIndex = LayerMask.NameToLayer(hitLayerName);

    //    //if (idx == hitObjLayerIndex)
    //    //{

    //    //}

}
