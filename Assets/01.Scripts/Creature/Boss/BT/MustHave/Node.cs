using System.Collections.Generic;
using static AllEnum;


//FSM���� state�� ���� ����. �� ���� ���Ѵٸ� interface�� �����ص��ɰ�.
public abstract class Node
{
    protected Boss owner; // ���¸� ���� ����
    protected NodeState state; //�� ����� ���� ����
    public Node ParentNode; //����ص� ��������.
                            //���� ��� �ڽĵ��� ��� ���°� �����߰ų�, �������� ���ư����Ҷ�
                            //�ٽ� �Ǻ��� �����ϰ� ������..
    protected List<Node> childrenNode = new List<Node>(); //���� �Ǻ��� �ڽĵ�.
    public Node()
    {
        ParentNode = null;
    }

    public Node(List<Node> children)
    {
        foreach (var item in children)
        {
            AttachChild(item);
        }
    }
    public void AttachChild(Node child) //���� �ڽ��� ����
    {
        childrenNode.Add(child);
        child.ParentNode = this;//�ڽĵ��� �θ�� ���� ��
    }

    public abstract NodeState Evaluate(); //���� �Ǻ�
}
