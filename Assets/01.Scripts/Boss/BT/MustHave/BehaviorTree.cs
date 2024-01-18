using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviorTree : MonoBehaviour
{
    Node rootNode; //���� �ֻ��� �θ���. �� �����Ǻ����� ����
    Boss owner;//�� Ʈ���� �θ� �Ǵ� ��ü

    public void SetInit()
    {
        owner = GetComponent<Boss>();

        rootNode = new SelectorNode
        (
            new List<Node>
            {
                // ���� ��� (�׾����� �ؾ� �Ұ͵�)
                new Die(owner),

                new SelectorNode
                (
                    new List<Node>
                    {
                        // ��Ž��
                        new CheckEnemy(owner),
                        
                        // ���� �Ÿ�üũ
                        new CheckAttack(owner),
                        new SelectorNode
                        (
                            new List<Node>
                            {
                                // ��ų
                                new UseSkill(owner),
                                // �⺻ ����
                                new Attack(owner),
                                // �̵�(�ٱ�, �ȱ�)
                                new Move(owner)
                            }
                        )
                    }
                ),

                //������ �ֱ�
                new Idle(owner)
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
                rootNode.Evaluate(); //�������� �����Ǻ�����.
            }
        }
    }
}
