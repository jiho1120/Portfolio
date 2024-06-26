using UnityEngine;

[CreateAssetMenu]
public class NewSOSkill : ScriptableObject
{
    public int index; // 고유번호
    public int lv;
    public float effect; // 효과 공격이면 공격력 힐이면 힐하는양 ... 
    public float duration; // 스킬 지속 시간
    public float cool; // 쿨타임
    public float mana; // 소모 마나
    
    
}
