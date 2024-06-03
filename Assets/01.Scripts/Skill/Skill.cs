using UnityEngine;
using static AllEnum;

public abstract class Skill : MonoBehaviour
{
    protected SkillData data;
    protected ObjectType caster;


    protected Skill(SkillData skillData, ObjectType caster)
    {
        data = skillData;
        this.caster = caster;
    }



    public abstract void ApplyEffect();
    public abstract void RemoveEffect();

    public float GetDuration() { return data.duration; }
    public float GetIndex() { return data.index; }


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
