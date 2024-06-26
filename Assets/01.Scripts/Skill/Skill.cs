using UnityEngine;
using static AllEnum;

public abstract class Skill : MonoBehaviour,Initialize
{
    protected SkillData skilldata;
    [SerializeField] NewSkillType skillType;// ��ų Ÿ��
    [SerializeField] SkillName skillName; // ��ų �̸�
    public ObjectType caster { get; private set; }
    [SerializeField] bool setParent; // ��ų�� �÷��̾ ����ٴ��� // �׷���Ƽ�� �׶��常 flase
    [SerializeField] bool inUse; //false�Ͻ� ��ų����


    // ��ų �������� �̰ɷ� 
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
