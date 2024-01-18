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

                //가만히 있기
                new Idle(owner),
                new SequenceNode
                (
                    new List<Node>
                    {
                        // 이동(뛰기, 걷기)
                        new Move(owner),
                        new SelectorNode
                        (
                            new List<Node>
                            {
                                // 스킬
                                new UseSkill(owner),
                                // 기본 공격
                                new Attack(owner),
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
