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

                //�� ������(��ų �¾�����)
                new Stun(owner),
                new SequenceNode
                (
                    new List<Node>
                    {
                        // �̵�(�ٱ�, �ȱ�, ���߱�)
                        new Move(owner),
                        // �տ��ִ��� üũ�ؼ� ��ų �����ų� �ָ� ������
                        new SelectorNode
                        (
                            new List<Node>
                            {
                                // ��ų
                                new UseSkill(owner),
                                // �⺻ ����
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
                rootNode.Evaluate(); //�������� �����Ǻ�����.
            }
        }
    }
}
