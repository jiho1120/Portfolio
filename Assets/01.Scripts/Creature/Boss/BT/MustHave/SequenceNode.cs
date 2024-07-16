using System.Collections.Generic;

//AND ����. �ϳ��� false�� �� false
public class SequenceNode : Node
{
    public SequenceNode() : base()
    {
    }

    public SequenceNode(List<Node> children) : base(children)
    {
    }

    public override AllEnum.NodeState Evaluate() 
    {
        bool isRunning = false;
        //�ڽĵ��� ���鼭 ����üũ��.
        //�ϳ��� ���հ��̸�
        //���̻� �ٸ��ڽĵ��� üũ�غ����ʰ� ����.
        foreach (Node node in childrenNode)
        {
            switch (node.Evaluate())
            {
                case AllEnum.NodeState.Running:
                    isRunning = true;
                    continue;

                case AllEnum.NodeState.Success:
                    continue;

                case AllEnum.NodeState.Failure:
                    return AllEnum.NodeState.Failure;
                default:
                    break;
            }
        }

        //���� ���� ���ð��� �����鼭 �װ� ������
        return state = isRunning ? AllEnum.NodeState.Running : AllEnum.NodeState.Success;
    }
}
