using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SOSkill : ScriptableObject
{
    public int index; // 고유번호
    public AllEnum.SkillType skillType; // 스킬 타입
    public AllEnum.PassiveSkillType PassiveSkillType; // 스킬 종류
    public AllEnum.SkillName SkillName; // 스킬 이름
    public Sprite icon; // 그림
    public float effect; // 효과 공격이면 공격력 힐이면 힐하는양 ... 
    public float duration; // 스킬 지속 시간
    public float cool; // 쿨타임
    public float mana; // 소모 마나
}
