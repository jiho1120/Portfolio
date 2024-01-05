using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveSkill : Skill
{
    private float colCenter = 7f;
    private float colSize = 12f;
    private float duration = 1f;
    float skillSpeed = 3;
    Coroutine boxCor = null;


    public override void DoSkill()
    {
        if (this == null)
        {
            Debug.LogError("����");
        }
        else
        {
            if (orgInfo.index == 1) // ��
            {
                KnockBackAttack();
            }
            else if (orgInfo.index == 2) // ������
            {

            }
            else if (orgInfo.index == 3) //��
            {

                if (boxCor == null)
                {
                    //Debug.Log("�ڽ� ���");
                    boxCor = StartCoroutine(GrowInBoxCollider());
                }
                //Debug.Log("�ڷ�ƾ����");

            }
            else if (orgInfo.index == 4) // �߷�
            {
                GravityAttack();
            }
        }
        StartCoroutine(DieTimer());
    }

    //���޶�� ��û
    IEnumerator DieTimer()
    {
        yield return new WaitForSeconds(orgInfo.duration);
        SkillManager.Instance.SetOffSkill(this);
    }

    public void KnockBackAttack()
    {
        Player plyer = GameManager.Instance.player.GetComponent<Player>();
        Collider[] colliders = Physics.OverlapSphere(this.transform.position, 8f);
        // �ݶ��̴��� ������ �� �Ÿ��� ���Ÿ� ���ؼ� �ݴ�� �о��
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].CompareTag("Monster"))
            {
                colliders[i].GetComponent<Monster>().TakeDamage(plyer.playerStat.criticalChance, plyer.playerStat.attack * this.orgInfo.effect);
                colliders[i].GetComponent<Monster>().isHit = true;

                Vector3 direction = colliders[i].transform.position - this.transform.position;

                Rigidbody enemyRigidbody = colliders[i].GetComponent<Rigidbody>();
                if (enemyRigidbody != null)
                {
                    enemyRigidbody.useGravity = false;
                    enemyRigidbody.AddForce(direction.normalized /** 10*/, ForceMode.Impulse);
                }
            }
        }
    }

    public IEnumerator GrowInBoxCollider()
    {
        Debug.Log("�Լ�����");
        BoxCollider col = transform.GetComponent<BoxCollider>();
        Debug.Log(col);
        if (col != null)
        {
            float elapsedTime = 0;
            float timecal = 0;
            while (elapsedTime <= duration)
            {
                elapsedTime += Time.deltaTime * skillSpeed;
                timecal = elapsedTime / duration;
                col.center = new Vector3(0, 0, colCenter * timecal); // �����Ѱ� ���� , ����� �����ص���
                col.size = new Vector3(1, 1, colSize * timecal);
                Debug.Log("Counter1: " + col.center + " | Counter2: " + col.size);
                yield return null;
            }
        }
        boxCor = null; // �˾Ƽ� ����
    }
    public void GravityAttack()
    {
        Player plyer = GameManager.Instance.player.GetComponent<Player>();
        Collider[] colliders = Physics.OverlapSphere(this.transform.position, 5f);
        // �ݶ��̴��� ������ �� �Ÿ��� ���Ÿ� ���ؼ� �ݴ�� �о��
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].CompareTag("Monster"))
            {
                colliders[i].GetComponent<Monster>().TakeDamage(plyer.playerStat.criticalChance, plyer.playerStat.attack * this.orgInfo.effect);
                colliders[i].GetComponent<Monster>().isHit = true;

                //colliders[i].GetComponent<Monster>().SetRenderTrMove(this.transform.position);
                //colliders[i].GetComponent<Monster>().Agent.isStopped = true;
                colliders[i].GetComponent<Monster>().Agent.baseOffset = 2f;
                //colliders[i].transform.position = Vector3.MoveTowards(colliders[i].transform.position, this.transform.position, 0.2f);
                //colliders[i].transform.position = Vector3.Lerp(colliders[i].transform.position, this.transform.position, 0.001f * Time.deltaTime);
                Vector3 velo = Vector3.zero;
                colliders[i].transform.position = Vector3.SmoothDamp(transform.position, this.transform.position, ref velo, 0.1f);






                //Vector3 direction = this.transform.position - colliders[i].transform.position;

                //Rigidbody enemyRigidbody = colliders[i].GetComponent<Rigidbody>();
                //if (enemyRigidbody != null)
                //{
                //    enemyRigidbody.AddForce(direction.normalized * 10, ForceMode.Impulse);
                //}
            }
        }
        // ������ y��ġ 0���� �̵�
    }

    public override void DoReset()
    {
        
    }


}
