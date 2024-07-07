using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static AllEnum;

public class Gravity : ActiveSkill, IPull
{
    private HashSet<Creature> hitMonsters = new HashSet<Creature>();

    private Collider[] colliders;
    float radius = 2f;
    float attackCool = 0.5f;
    Coroutine attCor = null;
    // 다시만들기
    float power = 10f;
    public override void Activate()
    {
        base.Activate();
        SetSKillPos();
    }
    public override void Deactivate()
    {
        transform.SetParent(GameManager.Instance.player.skillPos);
        if (attCor != null)
        {
            StopCoroutine(attCor);
            attCor = null;
        }
        foreach (var creature in hitMonsters)
        {
            creature.IsPull = false;
            creature.StopPull();
        }
        hitMonsters.Clear();
        base.Deactivate();
    }

    public void SetPullCondition(Creature cre)
    {
        hitMonsters.Add(cre);
        cre.PullPosition = transform.position;
        cre.PullPower = power;
        cre.IsPull = true;
    }

    public override void SetSKillPos()
    {
        this.transform.SetParent(null);

        // 플레이어의 현재 위치
        Vector3 currentPosition = GameManager.Instance.player.transform.position;

        // 플레이어 자식의 전방 벡터 (로컬 z축)
        Vector3 forwardDirection = GameManager.Instance.player.transform.GetChild(0).forward;

        // 앞 위치를 구하기 위해 전방 벡터에 거리 값을 곱하고 현재 위치에 더함
        Vector3 frontPosition = currentPosition + forwardDirection * 10 + Vector3.up * 5;
        transform.position = frontPosition;
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        Creature cre = other.GetComponent<Creature>();
        if (cre != null)
        {
            if (cre.IsPull == true)
            {
                return;
            }
            Debug.Log("컨디션함수 실행");
            SetPullCondition(cre);
        }
    }
    protected override void ImplementEffects()
    {
        base.ImplementEffects();
        if (attCor == null)
        {
            attCor = StartCoroutine(AttackCor());
        }
    }
    IEnumerator AttackCor()
    {
        while (true)
        {
            DetectEnemies();
            for (int i = 0; i < colliders.Length; i++)
            {
                Creature cre = colliders[i].GetComponent<Creature>();

                cre.TakeDamage(skilldata.effect);
            }
            yield return new WaitForSeconds(attackCool);
        }
        
    }
    private void DetectEnemies()
    {
        Debug.Log("감지");
        colliders = Physics.OverlapSphere(transform.position, radius, enemyLayer);
    }
    private void OnDrawGizmosSelected()
    {
        // 기즈모 색상을 설정합니다 (적색)
        Gizmos.color = Color.red;
        // 구체 형태의 기즈모를 현재 오브젝트 위치에 그립니다.
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    public override bool CheckUsableSkill(Creature caster)
    {
        if (skillName == SkillName.Gravity)
        {
            if (caster.Stat.ultimateGauge < caster.Stat.maxUltimateGauge)
            {
                return false;
            }
        }
        return base.CheckUsableSkill(caster);
    }
}
