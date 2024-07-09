using UnityEngine;

public class Ground : ActiveSkill
{
    int dotDealCount = 3; // ��Ʈ�� ��� �ٲ��� Ƚ�� �Ǹ� ��
    float againTime = 3f; // ���ʵڿ� �ٽ� �����ٲ���
    Transform originTr;
   

    public override void SetEnemyLayer(int enemyLayer)
    {
        base.SetEnemyLayer(enemyLayer);
        if (enemyLayer == GameManager.Instance.player.EnemyLayerMask)
        {
            originTr = GameManager.Instance.player.skillPos;
        }
        else
        {
            originTr = GameManager.Instance.boss.skillPos;
        }
    }
    public override void Activate()
    {
        SetSKillPos();
        base.Activate();
    }
    public override void Deactivate()
    {
        transform.SetParent(originTr);
        base.Deactivate();
    }
    public override void SetSKillPos()
    {
        transform.SetParent(null);
        Vector3 pos;
        Quaternion rot;
        if (enemyLayer == GameManager.Instance.player.EnemyLayerMask)
        {
            pos = GameManager.Instance.player.transform.position;
            rot = GameManager.Instance.player.transform.GetChild(0).rotation;
        }
        else
        {
            pos = GameManager.Instance.boss.transform.position;
            rot = GameManager.Instance.boss.transform.GetChild(0).rotation;
            
        }

        transform.position = pos;
        transform.rotation = rot;
    }
    protected override void OnTriggerEnter(Collider collision)
    {
        base.OnTriggerEnter(collision);
        Creature creature = collision.gameObject.GetComponent<Creature>();
        // ������ ������ ������ �ڷ�ƾ�� ���ƾ���
        if (creature != null)
        {
            creature.StartHitAttCor(skilldata.effect, againTime, dotDealCount);

        }

    }
    
}
