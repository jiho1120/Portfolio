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
    //    // ��ų�� ������ �Ǻ� // ����Ҷ� ������ �޾ƿ���

    //    // ��ų�� ���� �Ǻ�(������ ������)
    //    // �����ڰ� �÷��̾�� ���Ϲ̿� ���̾�� ���ؼ� ��


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
