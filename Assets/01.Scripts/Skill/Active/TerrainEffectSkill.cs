using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// 지형지물을 남겨서 그게 효과를 주는 스킬
public class TerrainEffectSkill : ActiveSkill
{

    protected override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);
        
    }
    // 엔터에서 딜 말고 도트딜
    protected virtual void OnCollisionStay(Collision collision)
    {
        base.OnCollisionEnter(collision);

    }
}
