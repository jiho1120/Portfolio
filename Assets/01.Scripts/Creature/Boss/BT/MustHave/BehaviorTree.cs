using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviorTree : MonoBehaviour
{
    Node rootNode; //가장 최상위 부모노드. 내 상태판별기의 메인
    Boss owner;//이 트리의 부모가 되는 본체

    public void SetInit()
    {
        owner = GetComponent<Boss>();

        rootNode = new SelectorNode
        (
            new List<Node>
            {
                // 죽음 노드 (죽었을때 해야 할것들)
                new Die(owner),

                //못 움직임(스킬 맞았을때)
                new Stun(owner),
                new SequenceNode
                (
                    new List<Node>
                    {
                        // 이동(뛰기, 걷기, 멈추기)
                        new Move(owner),
                        // 앞에있는지 체크해서 스킬 날리거나 주먹 날리기
                        new SelectorNode
                        (
                            new List<Node>
                            {
                                // 스킬
                                new UseSkill(owner),
                                // 기본 공격
                                new Attack(owner)
                            }
                        )
                    }
                )
            }
        );
    }

    void Update()
    {
        if (rootNode == null)
        {
            return;
        }
        else
        {
            if (owner.IsDead() == false)
            {
                rootNode.Evaluate(); //매프레임 상태판별진행.
            }
        }
    }
}
