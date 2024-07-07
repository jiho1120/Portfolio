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
    // �ٽø����
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

        // �÷��̾��� ���� ��ġ
        Vector3 currentPosition = GameManager.Instance.player.transform.position;

        // �÷��̾� �ڽ��� ���� ���� (���� z��)
        Vector3 forwardDirection = GameManager.Instance.player.transform.GetChild(0).forward;

        // �� ��ġ�� ���ϱ� ���� ���� ���Ϳ� �Ÿ� ���� ���ϰ� ���� ��ġ�� ����
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
            Debug.Log("������Լ� ����");
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
        Debug.Log("����");
        colliders = Physics.OverlapSphere(transform.position, radius, enemyLayer);
    }
    private void OnDrawGizmosSelected()
    {
        // ����� ������ �����մϴ� (����)
        Gizmos.color = Color.red;
        // ��ü ������ ����� ���� ������Ʈ ��ġ�� �׸��ϴ�.
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
