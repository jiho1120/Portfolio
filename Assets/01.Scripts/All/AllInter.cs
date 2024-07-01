using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public interface Initialize
{
    public void Activate(); //활성화 할때

    public void Deactivate(); //비활성화 할때
}

public interface IProduct
{
    // add common properties and methods here
    public string ProductName { get; set; }

    // customize this for each concrete product
    public void Init();// 처음 설정할것
    
}

public interface IEquipable
{
    void Equip(); // 장착 로직
}

public interface IUsable
{
    void Use(); // 사용 로직
}

public interface IAttack // 평타기반 공격
{
    void Attack();
    bool CheckCritical();
    float CriticalDamage(float att);
}

// 상태이상효과
public interface IStatusEffect
{
    bool IsKnockback { get; set; }
    bool IsPull { get; set; }
    void Knockback();
    void Pull(Vector3 targetPosition);
    IEnumerator StopForceMove(float seconds);

}

// 패시브 효과에 필요한 함수들
public interface IBuffAndDebuff
{
    void SetAtt(float effect);
    void StartDecreaseAttCor(float effect, float seconds);
    void StopDecreaseAttCor();
    void GetAttToData();

    void StartSetHPCor(float effect, float seconds);
    void StopSetHpCor();
    void TakeDamage(float att);
}

public interface IKnockback
{
    /// <summary>
    /// 스킬에 미는힘 power 변수 선언해야함, y값도 상황에 따라서 설정하기
    /// </summary>
    /// <param name="cre"></param>
    /// <param name="_forcePower"></param>
    void SetKnockbackCondition(Creature cre);
}

public interface IPull
{
    void SetPullCondition(Creature cre);
}

public interface IHit
{
    void Attack(Creature cre);
}

public interface IHeal
{

}

public interface IDecreaseAtt
{

}

public interface IIncreaseAtt
{

}






