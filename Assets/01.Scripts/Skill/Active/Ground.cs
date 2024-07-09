using UnityEngine;

public class Ground : ActiveSkill
{
    int dotDealCount = 3; // 도트딜 몇번 줄껀지 횟수 되면 끝
    float againTime = 3f; // 몇초뒤에 다시 피해줄껀지
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
        // 밟으면 몬스터의 데미지 코루틴이 돌아야함
        if (creature != null)
        {
            creature.StartHitAttCor(skilldata.effect, againTime, dotDealCount);

        }

    }
    
}
