using System.Collections.Generic;

//OR �� �����ϴ�. �ϳ��� true�� �� true==> �ϳ��� �����̸� �� ����.�� �ش��.
public class SelectorNode : Node
{
    public SelectorNode() : base()
    {
    }

    public SelectorNode(List<Node> children) : base(children)
    {
    }

    public override AllEnum.NodeState Evaluate() //�� �ڽĵ��� ���ǵ��� �ϳ��� �����ϰų� �ϳ��� �̹� �������̾��ٸ� ������ ����...
    {
        foreach (Node node in childrenNode)
        {
            switch (node.Evaluate())
            {
                case AllEnum.NodeState.Running:
                    return state = AllEnum.NodeState.Running;

                case AllEnum.NodeState.Success:
                    return state = AllEnum.NodeState.Success;

                case AllEnum.NodeState.Failure:
                    continue;
                default:
                    break;
            }
        }

        //���� ���� ���ð��� �����鼭 �װ� ������
        return state = AllEnum.NodeState.Failure;
    }
}
